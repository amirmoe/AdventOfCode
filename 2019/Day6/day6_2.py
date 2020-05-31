import sys

class Node(object):
    def __init__(self, data: str):
        self.object = data
        self.children = []
        self.parent = None

    def add_child(self, obj):
        self.children.append(obj)
    
    def add_parent(self, obj):
        self.parent = obj

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

def get_path_to_com(node: Node, node_list):
    parent = node.parent
    if parent.object != "COM":
        node_list.append(parent)
        node_list = get_path_to_com(parent, node_list)
    return node_list

def get_lca(first_list: list, second_list: list):  
    for i in first_list:
        for j in second_list:
            if i == j:
                return i
    return None


nodes = []
for line in sys.stdin:
    line = line.strip()
    objects = line.split(')')

    parent_node = next((x for x in nodes if x.object == objects[0]), None)
    if parent_node == None:
        parent_node = Node(objects[0])
        nodes.append(parent_node)

    child_node = next((x for x in nodes if x.object == objects[1]), None)
    if child_node == None:
        child_node = Node(objects[1])
        nodes.append(child_node)

    child_node.add_parent(parent_node)
    parent_node.add_child(child_node)


you = next((x for x in nodes if x.object == "YOU"), None)
santa = next((x for x in nodes if x.object == "SAN"), None)

path_from_you = []
path_from_you = get_path_to_com(you, path_from_you)
path_from_santa = []
path_from_santa = get_path_to_com(santa, path_from_santa)

lca = get_lca(path_from_you, path_from_santa)

if lca != None:
    to_you = path_from_you.index(lca)
    to_santa = path_from_santa.index(lca)
    print (to_santa+to_you)
    
else:
    print("Something went wrong with LCA")