let books = [];
let connection = null;

let bookIdToUpdate;

getdata();
setupSignalR();


function setupSignalR() {
     connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4356/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BookCreated", (user, message) => {
        getdata();
    });

    connection.on("BookDeleted", (user, message) => {
        getdata();
    });

    connection.on("BookUpdated", (user, message) => {
        getdata();
    });

    connection.onclose
        (async () => {await start();
        });
    start();

}

async function getdata() {
    await fetch('http://localhost:4356/book')
        .then(x => x.json())
        .then(y => {
            books = y;
            //console.log(books);
            display();
        });
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

function display() {
    document.getElementById('resultarea').innerHTML = "";
    books.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            `<tr>`+
        `<td>` + t.bookId + `</td>` +
        `<td>` + t.title + `</td>` + 
            `<td>` + t.publicationYear + `</td>` + 
            `<td>` + t.genre + `</td>` + 
        `<td>` + t.authorId + `</td>` +
        `<td><button type="button" onclick="remove(${t.bookId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.bookId})">Update</button></td>` +
            `</tr>`;
    });
}

function showupdate(id) {
    document.getElementById('booktitleU').value = books.find(t => t['bookId'] == id)['title'];
    document.getElementById('genreU').value = books.find(t => t['bookId'] == id)['genre'];
    document.getElementById('updateformdiv').style.display = 'flex';
    bookIdToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:4356/book/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
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

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let title = document.getElementById('booktitleU').value;
    let genre = document.getElementById('genreU').value;
    fetch('http://localhost:4356/book', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                bookId: bookIdToUpdate,
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