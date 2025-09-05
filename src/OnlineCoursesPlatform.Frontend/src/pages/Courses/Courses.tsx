import { useState, useEffect  } from "react";
import { Outlet, useLocation } from "react-router-dom";
import CoursesTabs from "./CoursesTabs";

const Courses = () => {
  const [sort, setSort] = useState("lastCreated");
  const [tag, setTag] = useState<string | null>(null);
  const [searchQuery, setSearchQuery] = useState("");

   const location = useLocation();

   
  useEffect(() => {
    if (location.pathname.includes("/app/courses/browse")) {
      setSort("lastCreated");
      setTag(null);
      setSearchQuery("");
    } else if (location.pathname.includes("/app/courses/started")) {
      setSort("lastCreated");
      setTag(null);
      setSearchQuery("");
    }
  }, [location.pathname]);

  return (
    <>
      <CoursesTabs sort={sort} setSort={setSort} tag={tag} setTag={setTag}  onSearch={setSearchQuery} />
      <Outlet context={{ sort, tag, searchQuery }} />
    </>
  );
};

export default Courses;
