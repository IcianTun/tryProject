2019

ml-agents: "12.0.1"

barracuda: "0.3.2-preview"

commands

mlagents-learn config/trainer_config.yaml --train --run-id=testPlayer301

pip install mlagents==0.12.1



observation 
	ตำแหน่งบอส 
	ตำแหน่งผู้เล่น 
	จำนวนพื้นการโจมตีของบอสที่กำลังเหยียบอยู่
	angle(playerForward,BossPosition)

	
Vector Action space: (Discrete) 4 Branches:
	Forward Motion (3 possible actions: Forward, Backwards, No Action)
	Side Motion (3 possible actions: Left, Right, No Action)
	Rotation (3 possible actions: Rotate Left, Rotate Right, No Action)
	Attack (3 possible actions: Melee, Range, No Action)

	
+- reward angle-90 ( if > 90 , reward = -(angle-90) ) ( if < 90 , reward =  +90-angle)
-reward ถ้ากำลังเหยียบพื้น aoe อยู่
-reward ตามเวลา (ทุกครั้งที่ update)
+reward เมื่อบอสลด
-reward เมื่อเลือดตัวเองลด

