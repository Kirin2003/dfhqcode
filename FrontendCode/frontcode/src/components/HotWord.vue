<template>
  <div class="common-layout">
    <el-container>
      <AsidePart1 />
      <el-container>
        <el-header>
          <el-input v-model="essaySearched" placeholder="论文信息">
            <template #append>
              <el-button @click="SearchEssays">
                <el-icon><search /></el-icon>
              </el-button>
            </template>
          </el-input>
        </el-header>
        <p class="sort" :sort="Sort">"{{ Sort }}"论文</p>
        <EssayList />
      </el-container>
    </el-container>
  </div>
</template>
<script>
import { Search } from "@element-plus/icons";
import store from "@/store/";
import AsidePart1 from "./AsidePart1.vue";
import EssayList from "./EssayList.vue";
//import page_size from "./const";
export default {
  name: "HotWord",
  components: {
    Search,
    AsidePart1,
    EssayList,
  },
  mounted() {
    store.commit("ConvertEssays", this.EssaysGroup);
  },
  data() {
    return {
      select: "", //如何排序论文
      EssaysGroup: [],
      Sort: "default",
      //EssayNum: this.Essays.length * 5,
      essaySearched: "",
    };
  },
  methods: {
    SearchEssays() {
      if (this.essaySearched != null) {
        store.commit("ConvertSearchInfo", this.essaySearched);
      }
    },
  },
};
</script>
<style scoped>
.essays {
  font-size: 20px;
}
.sort {
  font-size: 20px;
  font-weight: bold;
  margin-top: 0px;
}
.el-pagination {
  padding-left: 400px;
  padding-top: 20px;
  --el-pagination-font-size: 17px;
}
</style>
