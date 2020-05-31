import sys

def main():
  total_sum = 0

  for line in sys.stdin.readlines():
    mass = int(line.strip())
    total_sum+= ((mass//3)-2)

  print (total_sum)


main()
