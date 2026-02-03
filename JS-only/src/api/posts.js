export async function buscarPosts() {
  try {
    const response = await fetch("https://jsonplaceholder.typicode.com/posts?_limit=5");
    
    // Verificação de segurança (status 200-299)
    if (!response.ok) {
      throw new Error(`Erro de servidor: ${response.status}`);
    }

    const posts = await response.json();
    return posts; // Retorna os dados puros
  } catch (error) {
    console.error("Falha na requisição:", error.message);
    throw error; // Repassa o erro para quem chamou a função tratar
  }
}