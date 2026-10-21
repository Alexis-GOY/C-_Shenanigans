project based on the "wpf-calculator" made by azulcoding
original file can be found on https://www.azulcoding.com/wpf-calculator/
youtube video made by the creator avaiable: https://www.youtube.com/watch?v=uZlUTJg21-w

the point of this project is to observe and understand better WPF application by manipulating the original file and observe the result.

Personnal note about XALM:
similar to CSS, use of a lot of attribut to adapt to need the display. in this case divide the window on a 2 ROW grid with a height of 2 out 7 of the size of the window for the 1st row and the 
remainnning space (5/7) for the second row.

 _________      _______ 
|_________| -> |_______| sub-grid 1 (2 row)
|_________| -> |_______|
|_________| -> |_|_|_|_| sub-grid 2 (5 row + 4 column)
|_________| -> |_|_|_|_| 
|_________| -> |_|_|_|_| 
|_________| -> |_|_|_|_| 
|_________| -> |_|_|_|_|
			   [1 2 3 4]

The first ROW is composed of a 2x1 grid, 1st ROW the operation, 2nd the result
The second ROW is composed of a 5x4 grid that contain all the button (numbers and operations)
