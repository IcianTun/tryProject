Automatic Boss Enemy Generation in Game (2019)

My senior project. It is embarassing somehow but I think I should drop the final report [here](https://iciantun.github.io/SeniorProject/SeniorProj_FinalReport.pdf) as well

Most of content below are notes.

<br /><br />

the convergence is satisfied when the absolute value of fitness is constant across generation

maybe add avg_attack interval to fitness too,

 	case 1 : player position is safe (no touch)
		move toward safe position of INNER player then filter to closest to boss
	
	case 2 : player pos is not safe
		move toward safe position closest to player (if more than 1 , choose nearest to boss)

		
player ai not calculating rotation.


bool isCrossing = true;

					 10 11 
					 
0 1 2 3 4 5 6   7 8 9 A B 

A A B D C 0 0 | C B B A D 

0 A A B 0 D C | A B C A 0 

-------|   |-------|            ( 3)

	3 crossover point = 4, 6, 9 
	
0-Point1 , Point1-Point2 , Point2-Point3

0-3	, 6-9
	
--------------------------------------------------------------------
Boss

	- Attack1
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]
	- Attack2
	- Attack3 4 5


SubAttacks Params

	- DelayBeforeActive [0-2]
	- DelayBeforeNext [0-2]
	- AoeTimer [3-7]
	- Damage [5,25]

	
	Subattack at boss & Subattack at player
		- AoeType [Square or CIrcle]
	Subattack at Coordinate
		- CoordinateName [CoordinateName or none]
		if none 
			- xPos, zPos [-15,15]
	Subattack toward player
		+ plusDistance [-10,10]
		+ minLength [5,10]
		AoeType Square
	
		
Move Params

	- DelayBefore
	- timeToMove
	- DelayAfter

	Move to Coordinate (base)
		- CoordinateName [CoordinateName or none]
		if none 
			- xPos, zPos [-15,15]
	Move toward player
		+ distanceOffset [-10,10] ( same as plusDistance )


AoeType
	
	Square
		- xSize, zSize [4,30]
		- rotation [0,180]
	Circle
		- diameter [4,30]
	
	
	
Coordinate
	
	Inner NESW , NW NE SE SW
	Outer NESW , NW NE EN ES SE SW WS WN , Corner NE NW SE SW
