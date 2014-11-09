#version 330 core

out vec3 color;
in vec3 fragmentColor; //getting this from the vertex shader

void main() {
	color = fragmentColor;
}