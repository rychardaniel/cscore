"use client";

import { useHeader } from "@/app/context/headerContext";
import { Icon } from "@iconify/react";
import { Input } from "antd";
import { Dispatch, SetStateAction } from "react";

type SearchProps = {
    value: string;
    setValue: Dispatch<SetStateAction<string>>;
};

export function Search({ value, setValue }: SearchProps) {
    const { placeholderSearch } = useHeader();
    return (
        <Input
            prefix={<Icon icon="ci:search-magnifying-glass" className="text-(--gray)" />}
            styles={{
                root: { background: "var(--gray-light-1)" },
            }}
            placeholder={placeholderSearch}
            value={value}
            onChange={(e) => setValue(e.target.value)}
        />
    );
}
