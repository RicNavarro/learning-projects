"use client"

import { useState } from "react";
import {sculptureList} from "./carousel_data"

const blackButtonStyle = "rounded-full border border-solid border-black/[.08] px-5 transition-colors hover:border-transparent hover:bg-black/[.04] dark:border-white/[.145] dark:hover:bg-[#1a1a1a] md:w-[158px] cursor-pointe"


export default function TestingGrounds() {
    
    /* A1: "Carrossel" usando state (mas sem transições, só com trocas de imagem)*/

    const [index, setIndex] = useState(0);
    const [showMore, setShowMore] = useState(false);
    function handleNextClick() {
        
        if (index == (sculptureList.length - 1))
            setIndex(0)
        else
            setIndex(index + 1);
    }

    function handleMoreClick() {
        setShowMore(!showMore);
    }

    let sculpture = sculptureList[index];

    /* A2: Caixa de texto que modifica o conteúdo escrito conforme a inserção ocorre */

    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')

    function handleFirstNameChange(e) {
        setFirstName(e.target.value);
    }

    function handleLastNameChange(e) {
        setLastName(e.target.value);
    }

    function handleReset() {
        setFirstName('');
        setLastName('');
    }

    /* A3: Checklist*/

    interface Task{
        id: number;
        text: string;
        completed: boolean;
    }

    const [tasks, setTasks] = useState<Task[]>([]);
    const [inputValue, setInputValue] = useState("");
    const [editandoId, setEditandoId] = useState(null);
    const [textoEdicao, setTextoEdicao] = useState("");


    const adicionarTarefa = () => {
        if (!inputValue.trim()) return;
        
        const novaTarefa = {
            id: Date.now(),
            text: inputValue,
            completed: false
        };

        setTasks([
            ...tasks,
            novaTarefa
        ]);
        setInputValue("")
    };

    const alternarStatus = (id : number) => {
        setTasks(tasks.map(task => 
            task.id === id ? {
                ...task,
                completed: !task.completed
            } : task
        ));
    };


    const salvarEdicao = (id : number) => {
        setTasks(tasks.map(task => 
            task.id === id ? {
                ...task,
                text: textoEdicao
            } : task
        ))
        setEditandoId(null);
    };

    const removerTarefa = (id : number) => {
        setTasks(tasks.filter(task => task.id !== id ))
    };

    return (
    <main className="min-h-screen m-8">

        <h1 className="text-5xl">React Testing Grounds</h1>
        <p className="mt-5">Zona de testes e aprendizado em react.</p>

        {/*         A1      */}

        <div className="mt-10 w-150 p-4 rounded-md outline-2 outline-gray-500/30">
            <button onClick={handleNextClick} className={blackButtonStyle + " mb-4 mt-2"}>
                Next
            </button>
            <h2 className="text-3xl bold">
                <i>{sculpture.name} </i> 
                by {sculpture.artist}
            </h2>
            <h3>  
                ({index + 1} of {sculptureList.length})
            </h3>
            <button onClick={handleMoreClick} className={blackButtonStyle + " my-4"}>
                {showMore ? 'Hide' : 'Show'} details
            </button>
            {showMore && <p className="pb-4">{sculpture.description}</p>}
            <img 
                className="w-24"
                src={sculpture.url} 
                alt={sculpture.alt}
            />
        </div>

        {/*         A2      */}

        <div className="my-6 w-150 p-4 rounded-md outline-2 outline-gray-500/30">
            <form onSubmit={e => e.preventDefault()}>
            <input
                placeholder="First name"
                className="rounded-lg px-2 py-1 mr-3 outline-2 outline-white-500/100"
                value={firstName}
                onChange={handleFirstNameChange}
            />
            <input
                placeholder="Last name"
                className="rounded-lg px-2 py-1 mr-3 outline-2 outline-white-500/100"
                value={lastName}
                onChange={handleLastNameChange}
            />
            <h1 className="my-2 text-2xl">Hi, {firstName} {lastName}</h1>
            <button className={blackButtonStyle} onClick={handleReset}>Reset</button>
            </form>
        </div>
        
        {/*         A3      */}

        <div className="w-150 p-4 rounded-md outline-2 outline-gray-500/30">
            <h2 className="text-xl my-2">Minha Checklist</h2>
            <input 
                className=" my-2 mr-3 px-2 py-1 rounded-lg outline-2 outline-blue-500/70"
                value={inputValue} 
                onChange={(e) => setInputValue(e.target.value)} 
                placeholder="Nova tarefa..."
            />
            <button className={blackButtonStyle} onClick={() => adicionarTarefa()}>Adicionar</button>

            <ul>
                {tasks.map(task => (
                <li key={task.id} className="my-4" style={{ textDecoration: task.completed ? 'line-through' : 'none' }}>
                    <input
                        type="checkbox"
                        onChange={
                            () => {
                                alternarStatus(task.id)
                            }
                        }
                        className= "mr-2"
                    />
                    {editandoId === task.id ? (
                        <>
                        <input
                            type="text"
                            className="rounded-md px-2 py-1 bg-gray-800"
                            value={textoEdicao}
                            onChange={(e) => setTextoEdicao(e.target.value)}
                        />
                        <button className="text-sm ml-3 px-3 py-1 outline-1 rounded-md cursor-pointer hover:bg-white/[.1] transition-colors"
                                onClick={() => salvarEdicao(task.id)}>Salvar</button>
                        </>
                    ):(
                        <>
                        {task.text}
                        <button className="text-sm ml-3 px-3 py-1 outline-1 rounded-md cursor-pointer hover:bg-white/[.1] transition-colors"
                                onClick={() => {
                                    setEditandoId(task.id);
                                    setTextoEdicao(task.text)}}
                                >
                                Editar
                        </button>
                        </>
                    )}
                    <button className="text-sm mx-3 px-3 py-1 outline-1 rounded-md cursor-pointer hover:bg-white/[.1] transition-colors"
                        onClick={() => removerTarefa(task.id)}>Deletar</button>
                </li>
                ))}
            </ul>
        </div>


    </main>
  );
}
