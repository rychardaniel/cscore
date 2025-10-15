"use client";

import { Icon } from "@iconify/react";
import { Input } from "antd";
import { Dispatch, SetStateAction } from "react";

type SearchProps = {
    value: string;
    setValue: Dispatch<SetStateAction<string>>;
    placeholder: string;
};

export function Search({ value, setValue, placeholder }: SearchProps) {
    return (
        <Input
            prefix={<Icon icon="ci:search-magnifying-glass" className="text-(--gray)" />}
            styles={{
                root: { background: "var(--gray-light-1)" },
            }}
            placeholder={placeholder}
            value={value}
            onChange={(e) => setValue(e.target.value)}
        />
    );
}
