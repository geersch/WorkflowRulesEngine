using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Activities.Rules;

namespace SharedClassLibrary
{
    public static class RuleSetManager
    {
        /// <summary>
        /// Load a .rules resource (= manifestResource) from an assembly and
        /// apply the specified RuleSet against an instance of a specified type
        /// of object.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="manifestResource"></param>
        /// <param name="ruleSet"></param>
        /// <param name="objectType"></param>
        /// <param name="instance"></param>
        public static void ApplyRuleSet(Type assembly, String manifestResource,
            String ruleSet, Type objectType, Object instance)
        {
            //Load the embedded .rules resource (= manifestResource parameter)
            //from the specified assembly (= assembly parameter).            
            Assembly assemblyResource = Assembly.GetAssembly(assembly);
            Stream stream = assemblyResource.GetManifestResourceStream(manifestResource);

            //The RuleSet is serialized and saved in Xml format.
            using (XmlReader xmlReader = XmlReader.Create(new StreamReader(stream)))
            {
                //Deserialize the Xml string. The result is a RuleDefinitions object.
                //The RuleDefinitions class is a container for the .rules file that 
                //was just loaded. A .rules file can contain one or more RuleSets.
                WorkflowMarkupSerializer markupSerializer =
                    new WorkflowMarkupSerializer();
                RuleDefinitions ruleDefinitions =
                    markupSerializer.Deserialize(xmlReader) as RuleDefinitions;

                if (ruleDefinitions != null)
                {
                    //Extract the wanted RuleSet (= ruleSet parameter) from the 
                    //RuleDefinitions object.
                    if (ruleDefinitions.RuleSets.Contains(ruleSet))
                    {
                        RuleSet rs = ruleDefinitions.RuleSets[ruleSet];

                        //Next check if the rules within the RuleSet can be a applied to
                        //the specified type of object. 
                        RuleValidation validation = new RuleValidation(
                            objectType, null);

                        if (rs.Validate(validation))
                        {
                            //If the RuleSet is valid for the specified type of object, then
                            //execute it against an instance of this class.                            
                            RuleExecution execution =
                                new RuleExecution(validation, instance);
                            rs.Execute(execution);
                        }
                    }
                }
            }
        }
    }
}