

conn = new Mongo();
db = conn.getDB("CardstoreDb");

db.createCollection('Cards')

db.Items.insertMany([
    { 'Name': 'Design Patterns', 'Price': 54.93, 'Category': 'Computers', 'Author': 'Ralph Johnson' },
    { 'Name': 'Clean Code', 'Price': 43.15, 'Category': 'Computers', 'Author': 'Robert C. Martin' }
])

db.Items.find({}).pretty()

