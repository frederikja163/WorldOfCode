using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace WorldOfCode
{
    /// <summary>
    /// The CPU side control of the programs that run on the GPU
    /// </summary>
    public class Shader
    {
        /// <summary>
        /// The handle for the shader program in openTK
        /// </summary>
        private int _handle = -1;

        /// <summary>
        /// The shader currently bound
        /// </summary>
        private static Shader _bound;
        
        /// <summary>
        /// Initialize the shader from a given path
        /// </summary>
        /// <param name="shaderPath">Path of the shader</param>
        public void Init(string shaderPath)
        {
            //If the shader is not clean return
            if (!_isDisposed)
            {
                return;
            }
            _isDisposed = false;
            
            //Read the shader file
            string fragmentSource = null;
            string vertexSource = null;
            ShaderType shaderReadmode = ShaderType.VertexShaderArb;
            StreamReader file = new StreamReader(shaderPath);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                
                //Preprocess sourcecode line
                if (line.StartsWith("#"))
                {
                    int spacer = line.IndexOf(' ');
                    string cmd = line.Substring(1, spacer - 1);
                    string arg = line.Substring(spacer + 1);
                    switch (cmd)
                    {
                        //The cmd is telling us about what kind of shader this is
                        case "shader":
                            switch (arg)
                            {
                                //The following shader is a fragment shader
                                case "f":
                                case "frag":
                                case "fragment":
                                    shaderReadmode = ShaderType.FragmentShader;
                                    continue;
                                case "v":
                                case "vert":
                                case "vertex":
                                    shaderReadmode = ShaderType.VertexShader;
                                    continue;
                                default:
                                    Logger.Warn($"\"{arg}\" is not a valid shader type");
                                    break;
                            }
                            break;
                        //Version is handled by openGL
                        case "version":
                            break;
                        default:
                            Logger.Warn($"\"{cmd}\" is not a valid preprocesser command");
                            break;
                    }
                }

                //Add the source to the appropriate variable
                switch (shaderReadmode)
                {
                    case ShaderType.VertexShader:
                        vertexSource += line + '\n';
                        break;
                    case ShaderType.FragmentShader:
                        fragmentSource += line + '\n';
                        break;
                    default:
                        break;
                }
            }

            //Compiles a single shader and returns the handle
            int CompileShader(string source, ShaderType shaderType)
            {
                int shader = GL.CreateShader(shaderType);
                GL.ShaderSource(shader, source);
                GL.CompileShader(shader);
                string shaderInfoLog = GL.GetShaderInfoLog(shader);
                if (!string.IsNullOrWhiteSpace(shaderInfoLog))
                {
                    //TODO: See todo from the programInfoLog if nothing is there just remove this one
                    Logger.Error($"Shader {shaderType.ToString()} failed to load from file {shaderPath} with error: {shaderInfoLog}");
                }

                return shader;
            }

            //Compile all the shaders
            int vert = CompileShader(vertexSource, ShaderType.VertexShader);
            int frag = CompileShader(fragmentSource, ShaderType.FragmentShader);

            //Create the shader program
            _handle = GL.CreateProgram();
            
            //Attach the shaders to the program
            GL.AttachShader(_handle, vert);
            GL.AttachShader(_handle, frag);
            
            //Link the program
            GL.LinkProgram(_handle);
            string programInfoLog = GL.GetProgramInfoLog(_handle);
            if (!string.IsNullOrWhiteSpace(programInfoLog))
            {
                //TODO: Let this follow the proper logging message template and send messages directly to the logger
                //Should probably work by passing some logging context struct to the logger, it will then take care
                //Of the actual layout, this is just so the file and file name is displayed in the correct place
                Logger.Error($"Shader from file {shaderPath} failed to link with error: {programInfoLog}");
            }

            //After the program has been linked the shaders can be detached and deleted
            GL.DetachShader(_handle, vert);
            GL.DeleteShader(vert);
            GL.DetachShader(_handle, frag);
            GL.DeleteShader(frag);
        }

        /// <summary>
        /// Bind the shader so it is ready for use
        /// </summary>
        public void Bind()
        {
            _bound = this;
            GL.UseProgram(_handle);
        }
        
        /// <summary>
        /// Unbind the shader for protective reasons
        /// </summary>
        public void Unbind()
        {
            if (_bound != this) { return; }
            GL.UseProgram(0);
        }

        #region Uniforms

        

        #endregion Uniforms
        
        #region Dispose
        /// <summary>
        /// Has the shader been disposed
        /// </summary>
        private bool _isDisposed = true;
        /// <summary>
        /// Dispose the shader and clean up memory
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                GL.DeleteShader(_handle);
                Unbind();
                
                _handle = -1;
                _isDisposed = true;
            }
        }

        /// <summary>
        /// Always make sure we dispose correctly, this is a last measure
        /// </summary>
        ~Shader()
        {
            Dispose();
        }
        #endregion Dispose
    }
}