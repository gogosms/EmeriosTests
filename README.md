# Reto: "Encuentra la subcadena más larga presente en una matriz"

En esta instancia es porque ya solo nos queda indagar en tu capacidad analítica. Queremos
hacerlo pidiéndote que desarrolles un pequeño programa, en el lenguaje de programación de
tu elección.
El reto, si quiere aceptarlo consiste en...
Sea una matriz cuadrada de dos dimensiones, de caracteres (cualquiera), devuelva la cadena
de caracteres adyacentes iguales más larga.
Ejemplo:

					B, B, D, A, D, E, F
					B, X, C, D, D, J, K
					H, Y, I, 3, D, D, 3
					R, 7, O, Ñ, G, D, 2
					W, N, S, P, E, 0, D
					A, 9, C, D, D, E, F
					B, X, D, D, D, J, K

Debería devolver la cadena D, D, D, D, D, porque hay una diagonal de
D de longitud 5 que es la más larga.
Debes buscar en vertical, diagonal y horizontal.
El programa que escribas debe tomar la entrada de un archivo de texto plano o de la entrada
estándar (elige el método que te sea más cómodo) y debe sacar la cadena por salida estándar.
Puedes escribirlo en el lenguaje de programación que te sea más cómodo.

# Solucion realizada en Visual Studio
La solucion consta de 3 proyectos
	## 1.- Console (Emerios.Matrix.Console) - Presenta la solucion de la matrix antes mencionada.
	## 2.- Domain  (Emerios.Matrix.Domain)  - Contiene las clases y logica utilizadas para llevar a cabo el ejercicio
	## 3.- Tests   (Emerios.Matrix.Tests)   - Proyecto de Tests donde se testean cada uno de los metodos que fueron utilizados para la resolucion de la problematica

## La solucion no contiene librerias externas, vasta con hacer un restore desde el VisualStudio y compilar, ejecutar el Emerios.Matrix.Console