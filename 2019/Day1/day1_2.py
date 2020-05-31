import sys

def main():
  total_sum = 0

  for line in sys.stdin.readlines():
    mass = int(line.strip())

    remainder = mass
    while remainder > 0:
      remainder = ((remainder//3)-2)
      if remainder > 0:
        total_sum+=remainder

  print (total_sum)


main()
