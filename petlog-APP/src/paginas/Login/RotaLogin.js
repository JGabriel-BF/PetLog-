import React from "react";
import { Navigate } from "react-router-dom";

export function RotaLogin({children}) {
    const vendedor = JSON.parse(localStorage.getItem("vendedor"));

    if (vendedor == null)
    {
        return <Navigate to="/" />;
    }

    return children;
}