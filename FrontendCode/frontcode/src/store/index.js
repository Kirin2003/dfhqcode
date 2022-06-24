import { createStore } from "vuex";

export default createStore({
  state() {
    return {
      essayS: null, //某篇选中的论文
      EssaysGroup: [], //一页展示的一组论文10篇
      searchInfo: "", //查询论文的信息
      hotword: {}, //根据热词给予论文列表,会议名+热词
      userId: "",
      recommended: false,
    };
  },
  getters: {},
  mutations: {
    SelectEssay(state, essay) {
      state.essayS = essay;
    },
    ConvertEssays(state, essays) {
      state.EssaysGroup = essays;
    },
    ConvertSearchInfo(state, Info) {
      state.searchInfo = Info;
    },
    CovertSort(state, sort) {
      state.hotword = sort;
    },
    InverseRecommend(state) {
      state.recommended = !state.recommended;
    },
    CovertUserId(state, userId) {
      state.userId = userId;
    },
  },
  actions: {},
  modules: {},
});
