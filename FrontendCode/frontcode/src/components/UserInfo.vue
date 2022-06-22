<template>
  <div class="common-layout">
    <el-container>
      <el-aside width="200px">
        <el-avatar><user-filled /></el-avatar>
        <el-main>
          <p class="Title">历史记录</p>
          <p v-for="record in HistoryRecords" :key="record.id">
            {{ record.Content }}
          </p>
        </el-main>
      </el-aside>
      <el-container>
        <el-header>
          <p style="text-align: right">
            <el-icon class="elementRight" @click="RouteToHome"
              ><HomeFilled
            /></el-icon>
          </p>
          <p>推荐列表</p>
        </el-header>
        <el-main v-infinite-scroll="getNewRecomend">
          <el-table
            class="essays"
            :data="RecomendEssays"
            row-style="height: 50px"
          >
            <el-table-column prop="title" label="题目" />
            <el-table-column prop="writers" label="作者" />
          </el-table>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>
<script>
import { UserFilled, HomeFilled } from "@element-plus/icons";
import router from "@/router/index";
export default {
  name: "UserInfo",
  components: { UserFilled, HomeFilled },
  data() {
    return {
      HistoryRecords: [],
      UserName: "",
      RecomendedEssays: [],
    };
  },
  methods: {
    getNewRecomend() {},
    RouteToHome() {
      router.push({ name: "home" });
    },
  },
};
</script>
<style scoped></style>
