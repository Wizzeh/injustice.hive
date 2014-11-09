#version 330 core
layout (location = 0) in vec3 vertexPosition_modelspace;
layout (location = 1) in vec3 vertexColor;

out vec3 fragmentColor; //define an output thing

uniform mat4 MVP;
void main() {                  //We're transforming so W=1
	vec4 transformedVertex = MVP * vec4(vertexPosition_modelspace,1);
	gl_Position = transformedVertex;
	fragmentColor = vertexColor; //we're forwarding this to the fragment shader
}