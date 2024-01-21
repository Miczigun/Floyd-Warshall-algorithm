from random import randint
n:int = 256

with open("graph256.txt", "w") as file:
    file.write(f"{n} {(n-1)*29}\n")
    for i in range(1,n):
        for j in range(1, 30):
            direction = (i + j) % n
            if direction == 0:
                direction = i
            file.write(f"{i} {direction} {randint(100,1000)}\n")
