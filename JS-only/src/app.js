/*
"use strict";

let alturaTotal = document.getElementById("total-height")
let posicaoAtual = document.getElementById("current-scroll")
let buttonScroll = document.getElementById("btnTop")

let alturaReal = Math.max(
  document.body.scrollHeight, document.documentElement.scrollHeight,
  document.body.offsetHeight, document.documentElement.offsetHeight,
  document.body.clientHeight, document.documentElement.clientHeight
);

alturaTotal.textContent = alturaReal;

window.addEventListener('scroll', () =>{
  posicaoAtual.textContent = window.pageYOffset;
  
  if (window.pageYOffset >= 200){
    buttonScroll.style.display = 'grid';
  } else {
    buttonScroll.style.display = 'none';
  }
  
})

buttonScroll.addEventListener('click', () =>{
  window.scrollTo({
    top: 0,
    behavior: 'smooth',
  });
})

function go() {
  showCircle(150, 150, 100).then(div => {
    div.classList.add('message-ball');
    div.append("Hello, world!");
  });
}

function showCircle(cx, cy, radius, callback) {
  let div = document.createElement('div');
  div.style.width = 0;
  div.style.height = 0;
  div.style.left = cx + 'px';
  div.style.top = cy + 'px';
  div.className = 'circle';
  document.body.append(div);

  return new Promise((resolve, reject) =>{
  setTimeout(() => {
    div.style.width = radius * 2 + 'px';
    div.style.height = radius * 2 + 'px';

    div.addEventListener('transitionend', function handler() {
      div.removeEventListener('transitionend', handler);
      resolve(div);
    });
  }, 0);
  })
}
*/
/*// Suponha que este código está rodando diretamente no browser (sem type="module")
var usuario = "Carlos";

function saudar() {
  return "Olá, " + usuario;
}

globalThis.saudar();

// Tarefa A: Como você acessaria a função 'saudar' de forma "future-proof" através do objeto global?
// Tarefa B: Se mudarmos 'var usuario' para 'const usuario', o que acontece ao tentar acessar 'window.usuario'?
*/
/*
function makeCounter() {
    let count = 0;
  
    function counter(){

      return counter.count++;
    };

    counter.set = (value) => {
        return counter.count = value;
    };

    counter.decrease = () => {
        return counter.count -= 1;
    }


    counter.count = 0;

    return counter;
  }
  
  let counter = makeCounter();
  
  alert( counter() ); // 0
  alert( counter() ); // 1
  
  counter.set(10); // set the new count
  
  alert( counter() ); // 10
  
  counter.decrease(); // decrease the count by 1
  
  alert( counter() ); // 10 (instead of 11)
  */
/*
function sum(a){
    
  let total = a;

  function f(b){
    total += b
    return f;
  }

  f.toString = () => total;

  return f;
}
  
console.log(sum(1)(2))// == 3; // 1 + 2
console.log(sum(1)(2)(3))// == 6; // 1 + 2 + 3
console.log(sum(5)(-1)(2))// == 6
console.log(sum(6)(-1)(-2)(-3))// == 0
console.log(sum(0)(1)(2)(3)(4)(5))// == 15


function curry(f) {

  let numArgs = f.length;

  return function curried(...args) {

    for(let arg of args){

    }

    // 1. Check if the length of args is enough
    // 2. If yes, return f(...args)
    // 3. If no, return a function that collects MORE args and calls 'curried' again

  };
}

// How it should work:
//function sum(a, b, c) {
//  return a + b + c;
//}

let curriedSum = curry(sum);

console.log(curriedSum(1)(2)(3)); // 6
*/
/*
let divScroller = document.getElementById("example");

// We want the bit that's outside of view, but on the bottom
console.log(divScroller.scrollHeight - divScroller.scrollTop - divScroller.clientHeight);
*/
/*
let padlessDiv = document.createElement("div")
padlessDiv.style.width = '50px';
padlessDiv.style.height = '50px';
padlessDiv.style.overflowY = 'scroll';

document.body.append(padlessDiv)
console.log(padlessDiv.offsetWidth - padlessDiv.clientWidth);
padlessDiv.remove()
*/
//let divScroller = document.getElementById("example");

//let cssWidth = parseFloat(getComputedStyle(divScroller).width);
//console.log(cssWidth)
//let domWidth = divScroller.clientWidth;
//console.log(domWidth)


//console.log(cssWidth - domWidth);

//let ball = document.getElementById("ball");
//let field = document.getElementById("field");


//ball.style.left = Math.round((field.clientWidth/2) - ball.offsetWidth/2) + 'px';
//ball.style.top = Math.round((field.clientHeight/2) - ball.offsetHeight/2) + 'px';
/*
function preloadImages(sources, callback){
  
  let errorLoading = false;
  let counter = 0;

  for (content of sources){
    let img = document.createElement('img');
    img.src = content;

    img.onerror = () =>{
      counter++;
    }
    img.onload = () =>{
      counter++;
    }

    if (counter == sources.length){
      callback();
    }
  }

}

function loaded() {
  alert("Images loaded")
}



preloadImages(["https://www.w3schools.com/css/img_5terre.jpg", "https://www.w3schools.com/css/img_forest.jpg", "https://www.w3schools.com/css/img_lights.jpg"], loaded);

*/

/*
// 1. Synchronous Example
console.log("Start");
function syncGreet() { console.log("I am Sync"); }
syncGreet();
console.log("End");

// 2. Asynchronous Example (The Event Loop in action)
console.log("---");
console.log("Start Async");
setTimeout(() => {
    console.log("Timer Finished (Callback)");
}, 0); // Even with 0ms, this goes to the Callback Queue!
console.log("End Async");
*/
/*
function saudacao(nome){

  console.log(`Olá, ${nome}!`)

}

saudacao("Ricardo");
console.log("Fim do script")
*/
/*
function encomendarPizza(sabor, callback){

  console.log("Pedido Feito!")
  setTimeout(() => {
    callback(sabor)
  }, 3000);

}

encomendarPizza("Mussarela", sabor =>{
  console.log(`Sua Pizza de ${sabor} chegou!`)
})
console.log("Vou ler um livro enquanto espero")
*/

/*
function autenticarUsuario(id, callback){
  console.log("Autenticando usuário");
  setTimeout(() =>{
    let nome = (id == 123) ? "Ricardo" : "Desconhecido"
    callback(nome)
  }, 1000);
}

function buscarPosts(nomeUsuario, callback){
  console.log("Buscando posts");
  setTimeout(() =>{
    let post = (nomeUsuario == "Ricardo") ? "Aprendendo JS" : "Vazio"
    callback(post)
  }, 1000)
}

function carregarComentarios(postTitulo, callback){
  console.log("Carregando comentários");
  setTimeout(() =>{
    let comentario = (postTitulo == "Aprendendo JS") ? "Tá sendo tenso" : "----"
    callback(comentario)
  }, 1000)
}

autenticarUsuario(123, (nome) => {
  buscarPosts(nome, (post) => {
    carregarComentarios(post, (comentario) => {
      console.log(`Usuário [${nome}] postou [${post}] com o comentário [${comentario}]`)
    })
  })
})
*/


/*
function delay(ms) {
  return new Promise((resolve, reject) =>{
    setTimeout(() => resolve(), ms);
  })  // your code
}

delay(3000).then(() => alert('runs after 3 seconds'));
*/
/*
function getData(callback) {
  
  return new Promise((resolve, reject) => {
    const success = true;
    
    setTimeout(() => {
     if(success){
      resolve("Dados adquiridos")
     } 
     else{
      reject("Erro")
     }
    }, 1000);

  })

}

getData().then(
  resultado => alert(resultado),
  erro => alert(new Error(erro))
);
*/
/*
function verificarEstoque(item){

  return new Promise((resolve, reject) => {
    setTimeout(() => {
      if (item == "Notebook"){
        console.log("Item confirmado")
        resolve(4000);
      }
      else{
        reject("Sem item");
      }
    }, 1000)
  })

}

function processarPagamento(valor){

  return new Promise((resolve, reject) => {
    setTimeout(() => {
      console.log("Pagamento processado");
      resolve();
    }, 1000)
  })

}

function enviarPedido(){
  
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      console.log("Envio planejado");
      resolve();
    }, 1000)
  })

}

verificarEstoque("Notebook")
  .then(preco => processarPagamento(preco))
  .then(enviarPedido)
  .finally(() => alert("Operação concluída"));
*/
/*
function buscarUsuario(id) {
  return new Promise((resolve, reject) => {
    console.log(id)
    if (id == 1){
      resolve("Alice");
    }
    else{
      reject("Usuário não encontrado");
    }
  })
}

function buscarPosts(usuario){
  console.log(usuario)
  return new Promise((resolve)=>{
    resolve(['Nina','Daniel','Elias']);
  })
}

buscarUsuario(2)
.then(nome => buscarPosts(nome))
.then(posts => console.log(`Posts encontrados ${posts}`))
.catch(erro => new Error(erro))
.finally(() => alert("Operação finalizada"))
*/
/*
function carregarPerfil(){
  return new Promise((resolve, reject) => {
    setTimeout(() => resolve("Usuário: Carlos"), 1000)
  })
}

function carregarPosts(){
  return new Promise((resolve, reject) => {
    setTimeout(() =>resolve(["Post A", "Post B"]), 2000)
  })
}

function carregarConfiguracoes(){
  return new Promise((resolve, reject) => {
    setTimeout(() => resolve([{"tema" : "escuro"}]), 1500)
  })
}

Promise.all([
  carregarPerfil(),
  carregarPosts(),
  carregarConfiguracoes()
]).then(resultados => resultados.forEach(
  resposta => alert(`-- ${resposta} --`)
));
*/
/*
function enviarEmail(email){
  return new Promise((resolve, reject) => {
    setTimeout(()=>{
      if(email.includes("@") && email.includes(".")){
        resolve();
      }
      else{
        reject(email)
      }
    }, 1000)
  })
}

Promise.allSettled([
  enviarEmail("cliente1@test.com"),
  enviarEmail("email-ruim-e-nao-funcionante"),
  enviarEmail("cliente3@test.com")
]).then(responses => {
  responses.forEach((response) => {
    if (response.status === "rejected"){
      alert(`email inválido: ${response.reason}`)
    }
  })
});
*/
/*
function servidorBrasil(){
  return new Promise((resolve, reject) => {
    setTimeout(() => resolve("Brasil"), 500);
  })
}

function servidorEuropa(){
  return new Promise((resolve, reject) => {
    setTimeout(() => resolve("Europa"), 1200)
  })
}

Promise.race([
  servidorBrasil(),
  servidorEuropa()
]).then(response => {
  alert(`O vencedor foi ${response}`)
})
  */
/*
async function loadJson(url) {
  
  let response = await fetch(url)

  if (response.status == 200) {
    let json = await response.json() 
    return json;
  }
  
  throw new Error(response.status);
}


loadJson('https://javascript.info/no-such-user.json')
  .catch(alert); // Error: 404
  */
/*
class HttpError extends Error {
  constructor(response) {
    super(`${response.status} for ${response.url}`);
    this.name = 'HttpError';
    this.response = response;
  }
}

async function loadJson(url) {
  
  response = await fetch(url)
    if (response.status == 200) {
      let json = await response.json();
      return json;
    } else {
      throw new HttpError(response);
    }
}

// Ask for a user name until github returns a valid user
async function demoGithubUser() {
  
  
  let user = undefined;

  while(true){
    let name = prompt("Enter a name?", "iliakan");
    try{
      user = await loadJson(`https://api.github.com/users/${name}`)
      alert(`Full name: ${user.name}.`);
      return user;
    } catch(err){
      if (err instanceof HttpError && err.response.status == 404){
          alert("No such user, please reenter.");
      }
      else{
        throw err;
      }
    }
  }
  
  
}

demoGithubUser();
*/
/*
async function wait() {
  await new Promise(resolve => setTimeout(resolve, 1000));

  return 10;
}

function f() {

  wait().then(
    value => alert(value)
  );
  // ...what should you write here?
  // we need to call async wait() and wait to get 10
  // remember, we can't use "await"
}

f()
*/
/*
function delay(fn, ms) {
  return new Promise((resolve, reject) => {
    setTimeout(() => fn().then(resolve, reject), ms);
  });
}

async function run() {
  connect();

  try {
    await Promise.all([
      // these 3 parallel jobs take different time: 100, 200 and 300 ms
      // we use the `delay` helper to achieve this effect
      delay(() => database.query(true), 100),
      delay(() => database.query(false), 200),
      delay(() => database.query(false), 300)
    ]);
  } catch(error) {
    console.log('Error handled (or was it?)');
  }

  disconnect();
}

run();
*/
/*
async function getUsers(names) {
  

  let responses = await Promise.all(
    names.map(async (username) => {
      try{

        let response = await fetch(`https://api.github.com/users/${username}`);
        
        if (response.status != 200){
          return null;
        }
        
        let user = await response.json();
        return user;
      
      } catch (err){
        return null;
      }
    })
  );

  return responses;

}



let users = ['iliakan', 'remy', 'no.such.users']
getUsers(users).then(retornos => {console.log(retornos)})
*/
/*
async function carregarTarefas(){

  let ul = document.createElement("ul");

  try{
    let response = await fetch("https://jsonplaceholder.typicode.com/todos?_limit=5");

    //checagem de conexão
    if (!response.ok){
      return null;
    }

    let parsed = await response.json();

    for (elem of parsed){
      let li = document.createElement("li")
      li.textContent = elem.title;
      ul.append(li);
    }    

  } catch (err) {
    return null;
  }
  
  document.body.append(ul);
}

carregarTarefas()
*/
/*
async function buscadorLink(){

  try{
  
    let response = await fetch("https://jsonplaceholder.typicode.com/pagina-inexistente")
  
    if (!response.ok) {
      throw new Error (`Erro: ${response.status}`)
    }

  } catch (err) {
    console.log("Falha na conexão local")
  }




}

buscadorLink();
*/
/*
async function criarPost() {

  try{
    let response = await fetch("https://jsonplaceholder.typicode.com/posts", {

      method: 'POST',
      headers: { 
        'Content-type': 'application/json; charset=UTF-8' 
      },
      body: JSON.stringify({ title: 'Meu Novo Post', body: 'Conteúdo do post', userId: 1 })

    });

    if (!response.ok){
      throw new Error (`Erro: ${response.status}`)
    }

    let result = await response.json();
    console.log(`Parabéns! O usuário de id [${result.userId}] foi criado.`);

  } catch (err) {
    console.error(err);
  }

}

criarPost();
*/

/*
async function fetchWithTimeout(url, timeout) {

  let controller = new AbortController();
  const timer = setTimeout(() => controller.abort(), timeout)

  try {
    let response = await fetch(url, {
      signal: controller.signal
    })
    return response;
  }
  catch (err){
    throw(err)
  } finally {
    clearTimeout(timer);
  }

}

// Teste esperado:
fetchWithTimeout('https://api.github.com/users/iliakan', 10) // 10ms é muito pouco, deve abortar
  .then(resp => console.log("Sucesso!"))
  .catch(err => {
    if (err.name === 'AbortError') {
      console.log("A requisição demorou demais e foi abortada.");
    } else {
      console.log("Erro de rede comum.");
    }
  });
*/
/*
const apiManager = {
  baseUrl: 'https://api.exemplo.com/users',

  async updateUser(id, data) {
    console.log(`[LOG] Iniciando updateUser para o ID: ${id}`);
    const url = `${this.baseUrl}/${id}`;

    try {
      console.log(`[LOG] Enviando PUT para ${url}...`);
      let response = await fetch(url, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(data),
        credentials: "omit",
        mode: 'cors'
      });

      console.log(`[LOG] Resposta recebida. Status: ${response.status} (${response.statusText})`);

      if (!response.ok) {
        throw new Error(`Erro na atualização: ${response.status}`);
      }

      let result = await response.json();
      console.log(`[LOG] JSON processado com sucesso.`);
      return result;

    } catch (err) {
      console.error(`[ERROR] Falha detectada no método updateUser: ${err.message}`);
      throw err; // Propaga o erro para o .catch externo
    }
  },


  async deleteUser(id) {
    console.log(`[LOG] Iniciando deleteUser para o ID: ${id}`);
    const url = `${this.baseUrl}/${id}`; // Ajustado para incluir o ID

    try {
      console.log(`[LOG] Enviando DELETE para ${url} (keepalive ativo)...`);
      let response = await fetch(url, {
        method: 'DELETE',
        keepalive: true,
        mode: 'cors',
        credentials: 'omit'
      });

      console.log(`[LOG] Resposta da deleção recebida. Status: ${response.status}`);

      if (!response.ok) {
        throw new Error(`Erro na deleção: ${response.status}`);
      }

      const finalData = response.status === 204 ? null : await response.json();
      console.log(`[LOG] Deleção finalizada.`);
      return finalData;

    } catch (err) {
      console.error(`[ERROR] Falha detectada no método deleteUser: ${err.message}`);
      throw err;
    }
  }
};

// Testando o fluxo
console.log("[INÍCIO] Disparando chamadas da API...");

apiManager.updateUser(123, { name: "Novo Nome" })
  .then((data) => console.log("[SUCESSO] Fluxo de atualização completo:", data))
  .catch(err => console.error("[FALHA] Ocorreu um erro no processo:", err.message));
*/

// 1. Definição da lógica de busca (O que irá para api.js)
// 2. Definição da lógica de exibição (O que irá para ui.js)
import { buscarPosts } from "./api/posts.js";
import { exibirPostsNaTela } from "./ui/render.js";

// 3. Orquestração da execução (O que irá para main.js)
async function inicializarApp() {
  try {
    const dados = await buscarPosts();
    exibirPostsNaTela(dados);
  } catch (err) {
    const lista = document.getElementById("lista-posts");
    lista.innerHTML = `<li style="color: red">Não foi possível carregar os posts.</li>`;
  }
}

// Inicia o processo
inicializarApp();

