let books = [];
getdata();
async function getdata() {
    await fetch('http://localhost:4356/book')
        .then(x => x.json())
        .then(y => {
            books = y;
            console.log(books);
            display();
        });
}


function display() {
    //document.getElementById('resultarea').innerHTML = "";
    books.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            `<tr>`+
        `<td>` + t.bookId + `</td>` +
        `<td>` + t.title + `</td>` + 
            `<td>` + t.publicationYear + `</td>` + 
            `<td>` + t.genre + `</td>` + 
        `<td>` + t.authorId + `</td>` +
        `<button type="button" onclick="remove(${t.bookId})> Remove </button>`+
            `</tr>`;
    });
}

function remove(id) {
    alert(id);
}

function create() {
    let title = document.getElementById('booktitle').value;
    let genre = document.getElementById('genre').value;
    fetch('http://localhost:4356/book', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                title: title,
                genre: genre
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
    
}