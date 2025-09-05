import UniversalTabs from "../../components/common/UniversalTabs/UniversalTabs";
import CoursesFiltersPanel from "./CourseFiltersPanel";
import StartedCoursesFiltersPanel from "./Started/StartedCoursesFiltersPanel";

interface CoursesTabsProps {
  sort: string;
  setSort: (value: string) => void;
  tag: string | null;
  setTag: (tag: string | null) => void;
  onSearch: (query: string) => void;
}

const coursesTabs = [
  { label: "Browse", route: "/app/courses/browse" },
  { label: "Started", route: "/app/courses/started" },
  { label: "Completed", route: "/app/courses/completed" },
];

const CoursesTabs: React.FC<CoursesTabsProps> = ({
  sort,
  setSort,
  tag,
  setTag,
  onSearch,
}) => {
  return (
    <UniversalTabs
      tabs={coursesTabs}
      filtersPanel={(currentTab) => {
        switch (currentTab.route) {
          case "/app/courses/browse":
            return (
              <CoursesFiltersPanel
                onSortChange={setSort}
                onTagChange={setTag}
                onSearch={onSearch}
              />
            );
          case "/app/courses/started":
            return (
              <StartedCoursesFiltersPanel
                onSortChange={setSort}
                onTagChange={setTag}
                onSearch={onSearch}
              />
            );
          default:
            return null;
        }
      }}
    />
  );
};

export default CoursesTabs;
