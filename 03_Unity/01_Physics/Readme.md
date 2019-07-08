## Solution "Calculate the force to reach the destination"
The task is to calculate the force that needs to be applied to the rigid body to throw it to the destination.  
![alt text](https://3.bp.blogspot.com/-tGOUekat_OQ/VS50qGSXKNI/AAAAAAAAAbo/YhdmERtlzHo/s1600/formula.png "Formula for the range of artillery equipment at an angle to the horizon")  
From that formula we can calculate the needed force:  

```F = mass * SQRT(distance * g) * direction/ sin (2a);```  
   
Might be useful for highlighting the destination when you already selected the force magnitude and is going to use Physics.  
For drawing debug lines that take into account Physics.  
For actually throwing the projectiles, etc.
