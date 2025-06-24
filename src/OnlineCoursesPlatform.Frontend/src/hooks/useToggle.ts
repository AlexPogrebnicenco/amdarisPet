import { useState, useCallback } from "react";

export const useToggle = (initialState: boolean = false) : [boolean, (value?: boolean) => void] => {
    const [state, setState] = useState<boolean>(initialState);

    const toggle = useCallback((value?: boolean) => {
        if (typeof value === 'boolean') {
            setState(value);
        } else {
            setState(prevState => !prevState);
        }
    }, []);

    return [state, toggle];
};