import { useState, useEffect } from "react";
import { Outlet, useLocation } from "react-router-dom";
import TeacherTabs from "./TeacherTabs";

const Teacher = () => {
  const [sort, setSort] = useState("lastCreated");
  const [tag, setTag] = useState<string | null>(null);
  const [searchQuery, setSearchQuery] = useState("");

  const location = useLocation();

  useEffect(() => {
    if (location.pathname === "/app/teacher/my-courses") {
      setSort((prev) => {
        if (prev !== "lastCreated") return "lastCreated";
        return prev;
      });

      setTag((prev) => {
        if (prev !== null) return null;
        return prev;
      });

      setSearchQuery((prev) => {
        if (prev !== "") return "";
        return prev;
      });
    }
  }, [location.pathname]);

  return (
    <>
      <TeacherTabs
        sort={sort}
        setSort={setSort}
        tag={tag}
        setTag={setTag}
        onSearch={setSearchQuery}
      />
      <Outlet context={{ sort, tag, searchQuery }} />
    </>
  );
};

export default Teacher;
