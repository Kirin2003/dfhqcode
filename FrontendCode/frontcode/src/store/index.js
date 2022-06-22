import { createStore } from "vuex";

export default createStore({
  state() {
    return {
      essayS: null,
    };
  },
  getters: {},
  mutations: {
    SelectEssay(state, essay) {
      state.essayS = essay;
    },
  },
  actions: {},
  modules: {},
});
