
let users = [];

let connection = null;

let userIdToUpdate;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4356/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("UserCreated", (user, message) => {
        getdata();
    });

    connection.on("UserDeleted", (user, message) => {
        getdata();
    });

    connection.on("UserUpdated", (user, message) => {
        getdata();
    });

    

    connection.onclose
        (async () => {
            await start();
        });
    start();

}

async function getdata() {
    await fetch('http://localhost:4356/user')
        .then(x => x.json())
        .then(y => {
            users = y;
            //console.log(users);
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
    users.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            `<tr>` +
            `<td>` + t.userId + `</td>` +
            `<td>` + t.name + `</td>` +
            `<td>` + t.email + `</td>` +
            `<td>` + t.phone + `</td>` +
        `<td><button type="button" onclick="remove(${t.userId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.userId})">Update</button></td>` +
            `</tr>`;
    });
}

function showupdate(id) {
    document.getElementById('usernameU').value = users.find(t => t['userId'] == id)['name'];
    document.getElementById('emailU').value = users.find(t => t['userId'] == id)['email'];
    document.getElementById('phoneU').value = users.find(t => t['userId'] == id)['phone'];
    document.getElementById('updateformdiv').style.display = 'flex';
    userIdToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:4356/user/' + id, {
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
    let name = document.getElementById('username').value;
    let email = document.getElementById('email').value;
    let phone = document.getElementById('phone').value;
    fetch('http://localhost:4356/user', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: name,
                email: email,
                phone: phone
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
    let name = document.getElementById('usernameU').value;
    let email = document.getElementById('emailU').value;
    let phone = document.getElementById('phoneU').value;
    fetch('http://localhost:4356/user', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                userId: userIdToUpdate,
                name: name,
                email: email,
                phone: phone
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
