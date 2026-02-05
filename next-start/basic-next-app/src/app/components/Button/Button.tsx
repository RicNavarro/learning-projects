"use client";

import Link from "next/link";

interface ButtonProps {
    label : string;
    onClick: () => void;
}

const Button = ({label, onClick} : ButtonProps) => {
    return (
        <Link href={"/testing_grounds"}>
            <button 
                onClick={onClick}
                className="flex h-12 w-full items-center justify-center rounded-full border border-solid border-black/[.08] px-5 transition-colors hover:border-transparent hover:bg-black/[.04] dark:border-white/[.145] dark:hover:bg-[#1a1a1a] md:w-[158px] cursor-pointer"
            >
                Just a {label} button
            </button>
        </Link>
        );
};
export default Button;