import type { ReactNode } from "react";
import { useAuth } from "../../../context/AuthContext";

interface RoleBasedRenderProps {
  allowedRoles: string[];
  children: ReactNode;
  authorId?: number;
}

const RoleBasedRender = ({
  allowedRoles,
  children,
  authorId,
}: RoleBasedRenderProps) => {
  const { role, userId } = useAuth();

  if (!role || !allowedRoles.includes(role)) {
    return null;
  }

  if (role === "Teacher" && authorId !== undefined && authorId !== userId) {
    return null; // учитель, но не автор — не показываем
  }

  return <>{children}</>;
};

export default RoleBasedRender;
