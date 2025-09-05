import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../../context/AuthContext";
import { saveTokens } from "../../../services/tokenService";

const GoogleCallbackPage = () => {
  const navigate = useNavigate();
  const { setAuthState } = useAuth();

  useEffect(() => {
    const params = new URLSearchParams(window.location.search);
    const accessToken = params.get("accessToken");
    const avatarUrl = params.get("avatarUrl");

    if (accessToken) {
      saveTokens(accessToken);
      setAuthState(accessToken, avatarUrl);
    }

    navigate("/app/home");
  }, [navigate, setAuthState]);

  return <p>Logging in with Google...</p>;
};

export default GoogleCallbackPage;
