"use client"

import { useState } from "react";
import {sculptureList} from "./carousel_data"

export default function TestingGrounds() {
    
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

    const blackButtonStyle = "rounded-full border border-solid border-black/[.08] px-5 transition-colors hover:border-transparent hover:bg-black/[.04] dark:border-white/[.145] dark:hover:bg-[#1a1a1a] md:w-[158px] cursor-pointe"

    let sculpture = sculptureList[index];
    return (
    <main className="min-h-screen m-8">

        <h1 className="text-5xl">React Testing Grounds</h1>
        <p className="mt-5">Zona de testes e aprendizado em react.</p>

        <div className="mt-10">
            <button onClick={handleNextClick} className={blackButtonStyle + " my-4"}>
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



    </main>
  );
}
