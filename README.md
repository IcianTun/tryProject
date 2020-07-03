Automatic Boss Enemy Generation in Game (2019)

My senior project. 

I think this project is somehow disappointing(to me, atleast) so it embarassing somehow to show it. I heard that the department would upload my final report somewhere for the purpose that anyone could look up. So me uploading and linking [the report here](https://iciantun.github.io/SeniorProject/SeniorProj_FinalReport.pdf) too is ok, I think. As well as my [demo presentation](https://docs.google.com/presentation/d/1-LpOAVzhKmmsnFjFf8VxrmKYhA4AIPKMitujpmUnWXk/edit?usp=sharing), it has demo video of a result boss and gameplay as well.

Well, if anyone take a look and get some good idea or inspiration from this project, I would be very proud of you and thank you very much. 

Most of content below are notes.

<br /><br />

Player AI

 	case 1 : player position is safe (no touch)
		move toward safe position of INNER player then filter to closest to boss
	
	case 2 : player pos is not safe
		move toward safe position closest to player (if more than 1 , choose nearest to boss)

--------------------------------------------------------------------
Crossover

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
	
Additional Notes to myself	
	
the convergence is satisfied when the absolute value of fitness is constant across generation

maybe add avg_attack interval to fitness too,

