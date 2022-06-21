import axios from "axios";

const baseURL = "http://dfhqcode.cn";
const paper = axios.create({
  baseURL: baseURL + "/paper",
  timeout: 6000,
});

const searchPapers = (url) => {
  return paper
    .get("search/" + url)
    .then((res) => {
      return res;
    })
    .catch((error) => {
      console.log(error);
    });
};

export { searchPapers };
