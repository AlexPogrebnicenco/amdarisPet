import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { getAccessToken } from "../../../services/tokenService";

interface RequireAuthProps {
    children: React.ReactElement;
}

const RequireAuth = ({ children } : RequireAuthProps) => {
    const token = getAccessToken();
    const location = useLocation();

    if (!token) {
        return <Navigate to="/login" state={{ from: location }} replace />
    }

    return children;
};

export default RequireAuth;