using System;
using System.Reflection;
using UnityEngine;

namespace Snowy.Utils
{
    public static class Utilities
    {
        public static Vector3 AlignVectorWithRotation(Vector3 vector, Transform transform)
        {
            return transform.TransformVector(vector);
        }
        
        public static Vector3 Abs(Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }
        
        public static Vector3 GetClosestPosition(Vector3 pos, Vector3[] bounds)
        {
            var closest = bounds[0];
            var distance = Vector3.Distance(pos, bounds[0]);
            for (int i = 1; i < bounds.Length; i++)
            {
                var newDistance = Vector3.Distance(pos, bounds[i]);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    closest = bounds[i];
                }
            }
            return closest;
        }
        
        public static Vector3 GetPositionWithRespect(Transform current, Transform root)
        {
            if (root == null)
                return current.position;
            else
                return Quaternion.Inverse(root.rotation) * (current.position - root.position);
        }

        public static void SetPositionWithRespect(Transform current, Vector3 position, Transform root)
        {
            if (root == null)
                current.position = position;
            else
                current.position = root.rotation * position + root.position;
        }

        public static Quaternion GetRotationWithRespect(Transform current, Transform root)
        {
            //inverse(after) * before => rot: before -> after
            if (root == null)
                return current.rotation;
            else
                return Quaternion.Inverse(current.rotation) * root.rotation;
        }

        public static void SetRotationWithRespect(Transform current, Quaternion rotation, Transform root)
        {
            if (root == null)
                current.rotation = rotation;
            else
                current.rotation = root.rotation * rotation;
        }

        public static Array AddToArray(Array array, object element)
        {
            Array newArray = Array.CreateInstance(array.GetType().GetElementType(), array.Length + 1);
            Array.Copy(array, newArray, array.Length);
            newArray.SetValue(element, array.Length);
            return newArray;
        }
        
        public static Type GetType( string TypeName )
        {

            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, in the same assembly as the caller, etc.
            var type = Type.GetType( TypeName );

            // If it worked, then we're done here
            if( type != null )
                return type;

            // If the TypeName is a full name, then we can try loading the defining assembly directly
            if( TypeName.Contains( "." ) )
            {

                // Get the name of the assembly (Assumption is that we are using 
                // fully-qualified type names)
                var assemblyName = TypeName.Substring( 0, TypeName.IndexOf( '.' ) );

                // Attempt to load the indicated Assembly
                var assembly = Assembly.Load( assemblyName );
                if( assembly == null )
                    return null;

                // Ask that assembly to return the proper Type
                type = assembly.GetType( TypeName );
                if( type != null )
                    return type;

            }

            // If we still haven't found the proper type, we can enumerate all of the 
            // loaded assemblies and see if any of them define the type
            var currentAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            foreach( var assemblyName in referencedAssemblies )
            {

                // Load the referenced assembly
                var assembly = Assembly.Load( assemblyName );
                if( assembly != null )
                {
                    // See if that assembly defines the named type
                    type = assembly.GetType( TypeName );
                    if( type != null )
                        return type;
                }
            }

            // The type just couldn't be found...
            return null;

        }
    }
}