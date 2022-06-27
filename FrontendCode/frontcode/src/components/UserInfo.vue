<template>
  <div class="common-layout">
    <el-container>
      <el-aside width="200px">
        <UserPortrait />
        <el-main>
          <p class="Title">收藏论文</p>
          <p v-for="essay in CollectedEssays" :key="essay.id">
            {{ essay.Title }}
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
        </el-header>
        <p class="title">推荐列表</p>
        <EssayList />
      </el-container>
    </el-container>
  </div>
</template>
<script>
import { HomeFilled } from "@element-plus/icons";
import router from "@/router/index";
import EssayList from "./EssayList.vue";
import UserPortrait from "./UserPortrait.vue";
import store from "@/store";
import { getCollection } from "@/axios/axios";
export default {
  name: "UserInfo",
  components: { HomeFilled, EssayList, UserPortrait },
  data() {
    return {
      CollectedEssays: [],
      UserName: "",
      RecomendedEssays: [],
    };
  },
  mounted() {
    getCollection(this.userId).then((res) => {
      this.CollectedEssays = res.data;
    });
    store.commit("InverseRecommend");
  },
  methods: {
    getNewRecomend() {},
    RouteToHome() {
      router.push({ name: "home" });
    },
    RouteToEssayInfo() {
      //还要传递论文信息

      router.push({ name: "Essay" });
    },
  },
  computed: {
    userId: function () {
      return store.state.userId;
    },
  },
};
</script>
<style scoped>
.el-aside {
  width: 300px;
  height: 600px;
}
.elementRight {
  font-size: x-large;
}
.title {
  font-weight: bold;
  font-size: 25px;
}
</style>
