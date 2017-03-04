using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using UnityEngine;

public class RuntimeCompiler : MonoBehaviour
{
    static CompilerParameters param;
    static bool isInitialised = false;

    private static void Initialise()
    {
        param = new CompilerParameters();

        // Add ALL of the assembly references
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            param.ReferencedAssemblies.Add(assembly.Location);
        }

        // Add specific assembly references
        //param.ReferencedAssemblies.Add("System.dll");
        //param.ReferencedAssemblies.Add("CSharp.dll");
        //param.ReferencedAssemblies.Add("UnityEngines.dll");

        // Generate a dll in memory
        param.GenerateExecutable = false;
        param.GenerateInMemory = true;

        isInitialised = true;
    }

    public static Assembly Compile(string source)
    {
        Debug.Log(">>>>>>>>>>>>>>>> Start of Compile.");

        //When first called, initialise the assembly references first.
        //This should only occur once, else an exception is thrown.
        if (!isInitialised)
        {
            Initialise();
        }

        Debug.Log(">>>>>>>>>>>>>>>> After Initialise.");

        var provider = new CSharpCompiler.CodeCompiler();

        // Compile the source
        var result = provider.CompileAssemblyFromSource(param, source);

        Debug.Log(">>>>>>>>>>>>>>>> After compiling assembly from source.");

        //TODO: Debug
        if( result == null )
        {
            Debug.Log(">>>>>>>>>>>>>>>> result is null.");
        }

        Debug.Log(">>>>>>>>>>>>>>>> Trying to log result errors.");
        Debug.Log(result.Errors.Count);
        Debug.Log(">>>>>>>>>>>>>>>> After logging result errors.");

        //Check for errors
        if (result.Errors.Count > 0)
        {
            var msg = new StringBuilder();
            foreach (CompilerError error in result.Errors)
            {
                msg.AppendFormat("Error ({0}): {1}\n",
                    error.ErrorNumber, error.ErrorText);
            }

            Debug.Log(">>>>>>>>>>>>>>>> Errors:");
            Debug.Log(msg.ToString());
            Debug.Log(">>>>>>>>>>>>>>>> After errors.");

            throw new Exception(msg.ToString());
        }

        Debug.Log("End of Compile.");

        // Return the assembly
        return result.CompiledAssembly;
    }
}
