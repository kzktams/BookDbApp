let authorpopularities = [];
let mostloanedbooks = [];
let activities = [];
let searchauthor = [];
let between = [];
let bygenre = [];
let booksbyauthor = [];
let booksbyuser = [];
let connection = null;
getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4356/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GotBooksByAuthor", (user, message) => {
        getdata();
    });

    connection.on("GotMostLoanedBooks", (user, message) => {
        getdata();
    });

    connection.on("GotBooksByGenre", (user, message) => {
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
    await fetch('http://localhost:4356/Stat/AuthorPopularities')
        .then(x => x.json())
        .then(y => {
            authorpopularities = y;
            console.log(authorpopularities);
            display();
        });
    await fetch('http://localhost:4356/Stat/MostLoanedBooks')
        .then(x => x.json())
        .then(y => {
            mostloanedbooks = y;
            console.log(mostloanedbooks);
            display();
        });
    await fetch('http://localhost:4356/Stat/Activity')
        .then(x => x.json())
        .then(y => {
            activities = y;
            console.log(activities);
            display();
        });

    
}

function getbooksbyUser() {
    const uid = document.getElementById('userid');
    fetch('http://localhost:4356/Stat/BooksLoanedByUser/' + uid.value)
        .then(x => x.json())
        .then(y => {
            booksbyuser = y;
            console.log(booksbyuser);
            displayByUser();
        });
}

function displayByUser() {
    document.getElementById('byuser').style.display = 'flex';
    document.getElementById('byuserresult').innerHTML = "";
    booksbyuser.forEach(t => {
        document.getElementById('byuserresult').innerHTML +=
            `<tr>` +
        `<td>` + t.bookId + `</td>` +
        `<td>` + t.bookTitle + `</td>` +
        `<td>` + t.userName + `</td>` +
        `<td>` + formatDate(t.loanDate) + `</td>` +
        `<td>` + formatDate(t.returnDate) + `</td>` +
            `</tr>`;
    });
}

function getbooksbyauthor() {
    const id = document.getElementById('searchbooksbyauthor');
    fetch('http://localhost:4356/Stat/BooksByAuthor/' + id.value)
        .then(x => x.json())
        .then(y => {
            booksbyauthor = y;
            console.log(booksbyauthor);
            displayByAuthor();
        });
}

function displayByAuthor() {
    document.getElementById('byauthorresults').style.display = 'flex';
    document.getElementById('byauthorresult').innerHTML = "";
    booksbyauthor.forEach(t => {
        document.getElementById('byauthorresult').innerHTML +=
            `<tr>` +
            `<td>` + t.bookId + `</td>` +
            `<td>` + t.title + `</td>` +
            `<td>` + t.publicationYear + `</td>` +
            `<td>` + t.genre + `</td>` +
            `</tr>`;
    });
}
function getbygenre() {
    const genre = document.getElementById('genreselection');
    fetch('http://localhost:4356/Stat/BooksByGenre/' + genre.options[genre.selectedIndex].text)
        .then(x => x.json())
        .then(y => {
            bygenre = y;
            console.log(bygenre);
            displayByGenre();
        });
}

function displayByGenre() {
    document.getElementById('genre').style.display = 'flex';
    document.getElementById('searchbygenre').innerHTML = "";
    bygenre.forEach(t => {
        document.getElementById('searchbygenre').innerHTML +=
            `<tr>` +
            `<td>` + t.bookId + `</td>` +
            `<td>` + t.title + `</td>` +
            `<td>` + t.publicationYear + `</td>` +
            `<td>` + t.genre + `</td>` +
            `</tr>`;
    });
}
function getbetweendates() {
    const start = document.getElementById('startdate');
    const end = document.getElementById('enddate');
    fetch('http://localhost:4356/Stat/BooksLoanedBetweenDates/' + start.value + ',' + end.value)
        .then(x => x.json())
        .then(y => {
            between = y;
            console.log(between);
            displayBetween();
        });
}

function displayBetween() {
    document.getElementById('loandateresult').style.display = 'flex';
    document.getElementById('searchbetweendate').innerHTML = "";
    between.forEach(t => {
        document.getElementById('searchbetweendate').innerHTML +=
            `<tr>` +
            `<td>` + t.bookId + `</td>` +
            `<td>` + t.title + `</td>` +
        `<td>` + t.publicationYear + `</td>` +
            `<td>` + t.genre + `</td>` +
            `</tr>`;
    });
}

function getsearchauthor() {
    const searchauthorbyname = document.getElementById('searchauthorbyname');
    fetch('http://localhost:4356/Stat/AuthorsByName/' + searchauthorbyname.value)
        .then(x => x.json())
        .then(y => {
            searchauthor = y;
            console.log(searchauthor);
            displaySearchAuthor();
        });
}

function displaySearchAuthor() {
    document.getElementById('authorreults').style.display = 'flex';
    document.getElementById('searchauthorresult').innerHTML = "";
    searchauthor.forEach(t => {
        document.getElementById('searchauthorresult').innerHTML +=
            `<tr>` +
        `<td>` + t.authorId + `</td>` +
            `<td>` + t.name + `</td>` +
        `<td>` + formatDate(t.birthDate) + `</td>` +
        `<td>` + t.country + `</td>` +
        `<td>` + t.bookCount + `</td>` +
            `</tr>`;
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


function display() {
    document.getElementById('resultarea').innerHTML = "";
    authorpopularities.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            `<tr>` +
            `<td>` + t.authorId + `</td>` +
        `<td>` + t.authorName + `</td>` +
            `<td>` + t.loanCount + `</td>` +
            `</tr>`;
    });

    document.getElementById('mostloanedbooks').innerHTML = "";
    mostloanedbooks.forEach(t => {
        document.getElementById('mostloanedbooks').innerHTML +=
            `<tr>` +
        `<td>` + t.bookId + `</td>` +
            `<td>` + t.title + `</td>` +
            `<td>` + t.loanCount + `</td>` +
            `</tr>`;
    });

    document.getElementById('activities').innerHTML = "";
    activities.forEach(t => {
        document.getElementById('activities').innerHTML +=
            `<tr>` +
            `<td>` + t.userId + `</td>` +
            `<td>` + t.name + `</td>` +
            `<td>` + t.loanCount + `</td>` +
            `</tr>`;
    });
}