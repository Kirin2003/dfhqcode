<template>
  <el-table
    class="essays"
    :data="EssaysGroup"
    row-style="height: 100px"
    @row-click="RouteToEssayInfo"
  >
    <el-table-column prop="title" label="题目" />
    <el-table-column prop="authors" label="作者" />
  </el-table>
  <el-footer>
    <div>
      <el-pagination
        :page-size="page_size"
        :current-page="pageNo"
        layout="total, prev, pager, next"
        :total="totalCount"
        @current-change="handleCurrentChange"
        @next-click="pageNoplus"
        @prev-click="pageNoSub"
      >
      </el-pagination>
    </div>
  </el-footer>
</template>
<script>
import router from "@/router/index";
import store from "@/store/";
import {
  getPapers,
  getCount,
  searchPapers,
  getSortedPaper,
  recommend,
} from "@/axios/axios";
import { page_size } from "./const";
export default {
  name: "EssayList",
  data() {
    return {
      EssaysGroup: [],
      totalCount: 0,
      pageNo: 1,
    };
  },
  mounted() {
    getPapers(this.pageNo, page_size).then((res) => {
      this.EssaysGroup = res.data;
    });
    getCount().then((res) => {
      console.log(res);
      this.totalCount = res.data;
    });
  },
  methods: {
    RouteToEssayInfo(row) {
      this.essaySelected = row;
      store.commit("SelectEssay", this.essaySelected);
      //还要传递论文信息
      router.push({
        name: "Essay",
        params: { essayId: this.essaySelected.Id },
      });
    },
    handleCurrentChange() {
      getPapers(this.pageNo, page_size).then((res) => {
        this.EssaysGroup = res.data;
      });
      getCount().then((res) => {
        this.totalCount = res.data;
      });
    },
    pageNoPlus() {
      this.current++;
    },
    pageNoSub() {
      this.current--;
    },
  },
  computed: {
    searchInfo() {
      return store.state.searchInfo;
    },
    hotwordSort() {
      return store.state.hotword;
    },
    recommend() {
      return store.state.recommended;
    },
    userId() {
      return store.state.userId;
    },
  },
  watch: {
    searchInfo: function () {
      searchPapers(this.searchInfo, 1, page_size).then((res) => {
        this.EssaysGroup = res.data;
      });
      getCount().then((res) => {
        this.totalCount = res.data;
      });
    },
    hotwordSort: function () {
      getSortedPaper(
        this.hotwordSort.meeting,
        this.hotwordSort.word,
        1,
        page_size
      ).then((res) => {
        this.EssaysGroup = res.data;
      });
      getCount().then((res) => {
        this.totalCount = res.data;
      });
    },
    recommend: function () {
      recommend(this.userId).then((res) => {
        this.EssaysGroup = res.data;
      });
      getCount().then((res) => {
        this.totalCount = res.data;
      });
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
