

import { useState } from "react";
import axios from "axios";

const JDoodle_API_URL = "https://api.jdoodle.com/v1/execute";

const Projects = () => {
  const [language, setLanguage] = useState("python3");
  const [versionIndex, setVersionIndex] = useState("4"); // зависит от языка
  const [code, setCode] = useState("print('Hello from JDoodle!')");
  const [output, setOutput] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const runCode = async () => {
    setIsLoading(true);
    try {
      const response = await axios.post(JDoodle_API_URL, {
        clientId: "b0fb77cb4539274f85ed9c42a48b649c", // 🔁 ЗАМЕНИ
        clientSecret:
          "a2c23ac7058415ad33146dc274b7f1408a6156502775488b9463888ad08d6317", // 🔁 ЗАМЕНИ
        script: code,
        language: language,
        versionIndex: versionIndex,
      });

      setOutput(response.data.output);
    } catch (error) {
      if (error instanceof Error) {
        setOutput("Error: " + error.message);
      } else {
        setOutput("Unknown error occurred.");
      }
    }
    setIsLoading(false);
  };

  return (
    <div style={{ padding: 20, fontFamily: "sans-serif" }}>
      <h2>JDoodle Code Runner</h2>

      <label>
        Language:{" "}
        <select value={language} onChange={(e) => setLanguage(e.target.value)}>
          <option value="python3">Python 3</option>
          <option value="cpp17">C++ 17</option>
          <option value="java">Java</option>
          <option value="c">C</option>
          <option value="javascript">JavaScript (Node.js)</option>
          {/* добавь другие языки при необходимости */}
        </select>
      </label>

      <br />
      <textarea
        value={code}
        onChange={(e) => setCode(e.target.value)}
        placeholder="Write your code here..."
        style={{
          width: "100%",
          height: "300px",
          marginTop: 10,
          fontFamily: "monospace",
          fontSize: "14px",
        }}
      />

      <br />
      <button onClick={runCode} disabled={isLoading}>
        {isLoading ? "Running..." : "Run Code"}
      </button>

      <h3>Output:</h3>
      <pre style={{ background: "#f0f0f0", padding: 10 }}>{output}</pre>
    </div>
  );
};

export default Projects;
