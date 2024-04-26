


let authors = [];



let connection = null;


let authorIdToUpdate;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4356/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();


    connection.on("AuthorCreated", (user, message) => {
        getdata();
    });

    connection.on("AuthorDeleted", (user, message) => {
        getdata();
    });

    connection.on("AuthorUpdated", (user, message) => {
        getdata();
    });

    connection.onclose
        (async () => {
            await start();
        });
    start();

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



async function getdata() {
    await fetch('http://localhost:4356/author')
        .then(x => x.json())
        .then(y => {
            authors = y;
            //console.log(authors);
            displayA();
        });
}

function formatDate(dateString) {
    const date = new Date(dateString);
    let month = '' + (date.getMonth() + 1),
        day = '' + date.getDate(),
        year = date.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('.');
}

function displayA() {
    document.getElementById('resultareaA').innerHTML = "";
    authors.forEach(t => {
        document.getElementById('resultareaA').innerHTML +=
            `<tr>` +
            `<td>` + t.authorId + `</td>` +
            `<td>` + t.name + `</td>` +
            `<td>` + formatDate(t.birthDate) + `</td>` +
            `<td>` + t.country + `</td>` +
            `<td><button type="button" onclick="removeA(${t.authorId})">Delete</button>` +
            `<button type="button" onclick="showupdateA(${t.authorId})">Update</button></td>` +
            `</tr>`;
    });
}

function showupdateA(id) {
    document.getElementById('nameAU').value = authors.find(t => t['authorId'] == id)['name'];
    document.getElementById('bdAU').value = formatDate(authors.find(t => t['authorId'] == id)['birthDate']);
    document.getElementById('countryAU').value = authors.find(t => t['authorId'] == id)['country'];
    document.getElementById('updateformdivA').style.display = 'flex';
    authorIdToUpdate = id;
}

function removeA(id) {
    fetch('http://localhost:4356/author/' + id, {
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

function createa() {
    let name = document.getElementById('nameAC').value;
    let birthDay =new Date(document.getElementById('bdAC').value);
    let country = document.getElementById('countryAC').value;
    fetch('http://localhost:4356/author', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: name,
                birthDay: birthDay,
                country: country
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

function updateA() {
    document.getElementById('updateformdivA').style.display = 'none';
    let name = document.getElementById('nameAU').value;
    let birthDate = document.getElementById('bdAU').value;
    var parts = birthDate.split('.');
    var year = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10) - 1;
    var day = parseInt(parts[2], 10);
    var date = new Date(year, month, day);
    let country = document.getElementById('countryAU').value;
    fetch('http://localhost:4356/author', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                authorId: authorIdToUpdate,
                name: name,
                birthDate: date,
                country: country
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