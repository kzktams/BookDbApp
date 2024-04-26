let loans = [];

let connection = null;


let loanIdToUpdate;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4356/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("LoanCreated", (user, message) => {
        getdata();
    });

    connection.on("LoanDeleted", (user, message) => {
        getdata();
    });

    connection.on("LoanUpdated", (user, message) => {
        getdata();
    });


    connection.onclose
        (async () => {
            await start();
        });
    start();

}

async function getdata() {
    await fetch('http://localhost:4356/loan')
        .then(x => x.json())
        .then(y => {
            loans = y;
            //console.log(loans);
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
    loans.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            `<tr>` +
            `<td>` + t.loanId + `</td>` +
        `<td>` + t.userId + `</td>` +
            `<td>` + t.bookId + `</td>` +
            `<td>` + formatDate(t.loanDate) + `</td>` +
            `<td>` + formatDate( t.returnDate )+ `</td>` +
        `<td><button type="button" onclick="remove(${t.loanId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.loanId})">Update</button></td>` +
            `</tr>`;
    });
}

function showupdate(id) {
    document.getElementById('UserIdLU').value = loans.find(t => t['loanId'] == id)['userId'];
    document.getElementById('bookIdLU').value = loans.find(t => t['loanId'] == id)['bookId'];
    document.getElementById('loandateLU').value = formatDate(loans.find(t => t['loanId'] == id)['loanDate']);
    document.getElementById('returndateLU').value = formatDate(loans.find(t => t['loanId'] == id)['returnDate']);
    document.getElementById('updateformdiv').style.display = 'flex';
    loanIdToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:4356/loan/' + id, {
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
    let userId = document.getElementById('userIdL').value;
    let bookId = document.getElementById('bookIdL').value;
    let loandate= document.getElementById('loandateL').value;
    let returndate = document.getElementById('returndateL').value;
    fetch('http://localhost:4356/loan', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                userId: userId,
                bookId: bookId,
                loanDate: loandate,
                returnDate: returndate
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
    let userId = document.getElementById('UserIdLU').value;
    let bookId = document.getElementById('bookIdLU').value;
    let loandate = document.getElementById('loandateLU').value;
    var parts = loandate.split('.');
    var year = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10) - 1;
    var day = parseInt(parts[2], 10);
    var loan = new Date(year, month, day);
    let returndate = document.getElementById('returndateLU').value;
    var parts = returndate.split('.');
    var year = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10) - 1;
    var day = parseInt(parts[2], 10);
    var returnD = new Date(year, month, day);
    fetch('http://localhost:4356/loan', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                loanId: loanIdToUpdate,
                userId: userId,
                bookId: bookId,
                loanDate: loan,
                returnDate: returnD
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