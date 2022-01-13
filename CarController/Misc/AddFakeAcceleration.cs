    public float maxFwdSpeed = 200f;
    public float fwdSpeed = 0f;
    
    public float fwdAccel = 100f;
    public float stoppingAccel = 200f;
    
       // Added Acceleration to Forward Direction
        if (moveInput > 0) // if your forward input is greater than zero
        {
            if (fwdSpeed < maxFwdSpeed) // then we want to check if our fwdSpeed is less than our maxSpeed
            {
                fwdSpeed += Time.deltaTime * fwdAccel; // if it is then increase it with our acceleration as the multiplier
            }
            else // if its greater than our maxSpeed just set it to our maxSpeed
            {
                fwdSpeed = maxFwdSpeed;
            }
        }
        else // now if our forward input is less than or = to zero (we're not moving) so we want to decelerate instead
        {
            if (fwdSpeed > 0) // so we'll check if we do got forward speed
            {
                fwdSpeed -= Time.deltaTime * stoppingAccel; // and if we do we to subtract instead by our stopping acceleration
            }
        }
