import sys

class Node(object):
    def __init__(self, data: str):
        self.object = data
        self.children = []

    def add_child(self, obj):
        self.children.append(obj)

def number_of_children(node: Node) -> int:
    children_list = [node]
    count = 0
    while len(children_list) > 0:
        current_node = children_list.pop(0)
        count += len(current_node.children)
        children_list.extend(current_node.children)
    return count

def get_checksum(node_list: list) -> int:
    checksum = 0
    for node in node_list:
        checksum += number_of_children(node)
    return checksum

nodes = []
for line in sys.stdin:
    line = line.strip()
    objects = line.split(')')

    new_node = next((x for x in nodes if x.object == objects[0]), None)
    if new_node == None:
        new_node = Node(objects[0])
        nodes.append(new_node)

    existing_node = next((x for x in nodes if x.object == objects[1]), None)
    if existing_node == None:
        existing_node = Node(objects[1])
        nodes.append(existing_node)

    new_node.add_child(existing_node)


print (get_checksum(nodes))
