/* eslint-disable no-unused-vars */
import { config } from "@vue/test-utils";
import axios from "axios";
import { tr } from "element-plus/es/locale";

const BaseURL = "http://localhost:8080/";

//论文
const paper = axios.create({
  baseURL: BaseURL + "api/paper/",
  timeout: 6000,
});

const searchPapers = async (q, page, page_size) => {
  try {
    const res = await paper.get(
      "search_by_keywords?q=&{q}&page=${page}&page_size=${page_size}"
    );
    return res;
  } catch (error) {
    console.log(error);
  }
};
const getSortedPaper = async (meeting, keywords, page, page_size) => {
  try {
    const res = await paper.get(
      "search?meeting=${meeting}&keywords=${keywords}&page=${page}&page_size=${page_size}"
    );
    return res;
  } catch (error) {
    console.log(error);
  }
};
const getCount = async () => {
  try {
    const res = await paper.get("get_count");
    return res;
  } catch (error) {
    console.log(error);
  }
};

const getPapers = async (page, page_size) => {
  try {
    const res = await paper.get("page=${page}&page_size=${page_size}");
    return res;
  } catch (error) {
    console.log(error);
  }
};

//用户
const user = axios.create({ baseURL: BaseURL + "api/user/", timeout: 6000 });
const login = async (name, password) => {
  try {
    const res = await user.post("login?name=${name}&password=${password}");
    return res;
  } catch (error) {
    console.log(error);
  }
};
//const login=(param)=>{return user.post("",param).then((res)=>)}

//github
const github = axios.create({
  baseURL: BaseURL + "api/github/",
  timeout: 6000,
});
const getGithubInfo = async (id) => {
  try {
    const res = await github.get(id);
    return res;
  } catch (error) {
    console.log(error);
  }
};

//引用
const reference = axios.create({ baseURL: BaseURL + "paper/", timeout: 6000 });
const getCitation = async (paper_id, page, page_size) => {
  try {
    const res = await reference.get(
      "${paper_id}/citation?page=${page}&page_size=${page_size}"
    );
    return res;
  } catch (error) {
    console.log(error);
  }
};
const getReference = async (paper_id, page, page_size) => {
  try {
    const res = await reference.get(
      "${paper_id}/references?page=${page}&page_size=${page_size}"
    );
    return res;
  } catch (error) {
    console.log(error);
  }
};

//收藏
const collection = axios.create({
  baseURL: BaseURL + "api/collection/",
  timeout: 6000,
});
const getCollection = async (userId) => {
  try {
    const res = await collection.get(userId);
    return res;
  } catch (error) {
    console.log(error);
  }
};
const addCollection = async (info) => {
  try {
    const res = await collection.post("", info);
    return res;
  } catch (error) {
    console.log(error);
  }
};
const subColletion = async (info) => {
  try {
    const res = await collection.delete("", info);
    return res;
  } catch (error) {
    console.log(error);
  }
};
const judgeCollection = async (info) => {
  try {
    const res = await collection.get("${info.userId}/${info.paperId");
    return res;
  } catch (error) {
    console.log(error);
  }
};

//喜欢
const Like = axios.create({
  baseURL: BaseURL + "api/like/",
  timeout: 6000,
});
const addLike = async (info) => {
  try {
    const res = await Like.post("", info);
    return res;
  } catch (error) {
    console.log(error);
  }
};
const subLike = async (info) => {
  try {
    const res = await Like.delete("", info);
    return res;
  } catch (error) {
    console.log(error);
  }
};
const judgeLike = async (info) => {
  try {
    const res = await Like.get("${info.userId}/${info.paperId");
    return res;
  } catch (error) {
    console.log(error);
  }
};

//推荐
const recommendation = axios.create({ baseURL: BaseURL + "recommendation/" });
const recommend = async (userId) => {
  try {
    const res = await recommendation.get(userId);
    return res;
  } catch (error) {
    console.log(error);
  }
};

//热词
const hotword = axios.create({ baseURL: BaseURL + "hotwords/", timeout: 6000 });
const getHotword = async () => {
  try {
    const res = await hotword.get("");
    return res;
  } catch (error) {
    console.log(error);
  }
};
const test = axios.create({ baseURL: "http://localhost:8081", time: 6000 });
const maketest = async (url) => {
  try {
    const res = await test.get(url);
    return res;
  } catch (error) {
    console.log(error);
  }
};
export { searchPapers, getPapers, getCount, getHotword };
