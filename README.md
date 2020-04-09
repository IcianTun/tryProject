2019

Boss

	- Attacks1
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]
	- Attacks2
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]
		- Action [SubAttack or Move or Blank]


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
