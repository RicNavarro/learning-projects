export function exibirPostsNaTela(posts) {
    const lista = document.getElementById("lista-posts");

    if (!lista){
        ul = document.createElement('ul');
        ul.setAttribute("id", "lista-posts");
        document.body.append(ul);
    }

    lista.innerHTML = ""; // Limpa a lista antes de renderizar

    posts.forEach(post => {
    const li = document.createElement("li");
    li.style.marginBottom = "10px";
    li.innerHTML = `<strong>${post.title}</strong><p>${post.body}</p>`;
    lista.appendChild(li);
  });
}