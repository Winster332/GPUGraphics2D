using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

using Bitmap = System.Drawing.Bitmap;
using Color = System.Drawing.Color;
using Rectangle = System.Drawing.Rectangle;
using sGraphics = System.Drawing.Graphics;

namespace GPUGraphics2D {
    public class Graphics2D
    {
        public int TextureID { get; set; }
        public Bitmap Image { get; set; }
        public sGraphics Graphics { get; set; }
        public VertexBuffer VertexBuffer { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Graphics2D(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Image = new Bitmap(Width, Height);
 
            TextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 
                Image.Width, Image.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            VertexBuffer = new VertexBuffer(VertexFormat.XYZ_UV);
            VertexBuffer.AddVertex(0, 0, 0,  0, 0);
            VertexBuffer.AddVertex(Width, 0, 0,  1, 0);
            VertexBuffer.AddVertex(Width, Height, 0,  1, 1);
            VertexBuffer.AddVertex(0, Height, 0,  0, 1);
            VertexBuffer.AddIndices(new ushort[] { 0, 1, 2, 3 });
            VertexBuffer.Load();
        }
        public void Resize(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Image = new Bitmap(Width, Height);
            
            VertexBuffer.Dispose();

            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 
                Image.Width, Image.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            VertexBuffer = new VertexBuffer(VertexFormat.XYZ_UV);
            VertexBuffer.AddVertex(0, 0, 0,  0, 0);
            VertexBuffer.AddVertex(Width, 0, 0,  1, 0);
            VertexBuffer.AddVertex(Width, Height, 0,  1, 1);
            VertexBuffer.AddVertex(0, Height, 0,  0, 1);
            VertexBuffer.AddIndices(new ushort[] { 0, 1, 2, 3 });
            VertexBuffer.Load();
        }
        public void BeginRender()
        {
            this.Graphics = sGraphics.FromImage(Image);
        }
        public void EndRender() 
        {
            this.Graphics.Dispose();

            BitmapData data = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height),
            ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Image.Width, Image.Height, 0,
            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            Image.UnlockBits(data);

            GL.Enable(EnableCap.Texture2D);

            GL.PushMatrix();

            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
 
            GL.PushMatrix();
            Matrix4 ortho = Matrix4.CreateOrthographicOffCenter(0, Width, Height, 0, -1, 1);
            GL.LoadMatrix(ref ortho);


            VertexBuffer.Bind(PrimitiveType.Quads);

            GL.PopMatrix();

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();

            GL.Disable(EnableCap.Texture2D);
        }
    }
}