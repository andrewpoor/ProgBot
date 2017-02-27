using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using UnityEngine;

public class GroundController : MonoBehaviour {

    public float angularSpeed = 1.0f;

    //Contains the code to rotate the stage
    private System.Reflection.Assembly movementAssembly;
    private string scriptPath;

    void Start ()
    {
        scriptPath = Application.dataPath + "/userScripts/groundMovement.txt";
        string script = Load(scriptPath);
        if ( script == null )
        {
            throw new FileLoadException();
        }
        
        movementAssembly = RuntimeCompiler.Compile(script);

        //movementAssembly = RuntimeCompiler.Compile(@"
        //    using unityengine;

        //      public class groundcontroller
        //{
        //    public static float angularspeed = 50.0f;

        //    public static bool move(monobehaviour mb)
        //    {
        //        float rotatex = input.getaxis(""vertical"");
        //        float rotatez = -input.getaxis(""horizontal"");

        //        vector3 rotation = new vector3(rotatex, 0.0f, rotatez) * angularspeed;

        //        mb.transform.rotate(rotation * time.deltatime);

        //        return true;
        //    }
        //}
        //");
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //float rotateX = Input.GetAxis("Vertical");
        //float rotateZ = -Input.GetAxis("Horizontal");

        //Vector3 rotation = new Vector3(rotateX, 0.0f, rotateZ) * angularSpeed;

        //transform.Rotate(rotation * Time.deltaTime);

        if (movementAssembly != null)
        {
            var method = movementAssembly.GetType("GroundController").GetMethod("Move");
            var del = (Func<MonoBehaviour, bool>)Delegate.CreateDelegate(typeof(Func<MonoBehaviour, bool>), null, method);
            del.Invoke(this);
        }
    }

    public void reloadScript()
    {
        string script = Load(scriptPath);
        movementAssembly = RuntimeCompiler.Compile(script);
    }
 
    //Loads a file from storage
    private string Load(string fileName)
    {
        try
        {
            string line;
            string result = "";
            StreamReader reader = new StreamReader(fileName, Encoding.Default);

            using (reader)
            {
                do
                {
                    line = reader.ReadLine();

                    if (line != null)
                    {
                        result = result + line;
                    }
                }
                while (line != null);
                   
                reader.Close();
                return result;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
