<template>
  <div class="common-layout">
    <el-container>
      <AsidePart1 />
      <!-- 论文信息 -->
      <el-container>
        <el-header>
          <p style="text-align: right">
            <el-icon class="elementRight" @click="Like">
              <ElemeFilled v-if="liked" />
              <Eleme v-else />
            </el-icon>
            <el-icon class="elementRight" @click="Collect">
              <StarFilled v-if="collected === true" />
              <Star v-else />
            </el-icon>
            <el-icon class="elementRight" @click="RouteToHome"
              ><HomeFilled
            /></el-icon>
          </p>
        </el-header>
        <div>
          <p class="title">
            {{ essay.title }}
          </p>
        </div>
        <p>
          作者：
          {{ essay.Authors }}
        </p>
        <br />
        摘要
        <br />
        <div>{{ essay.PaperAbstract }}</div>
        <el-collapse>
          <el-collapse-item title="DOI" name="3">
            <div>{{ essay.Doi }}</div>
          </el-collapse-item>
          <el-collapse-item title="发表日期" name="4">
            <div>{{ essay.PaperDate }}</div>
          </el-collapse-item>
          <el-collapse-item title="pdf链接" name="5">
            <div>{{ essay.PdfHref }}</div>
          </el-collapse-item>
          <el-collapse-item title="来源会议" name="6">
            <div>{{ essay.Meeting }}</div>
          </el-collapse-item>
          <el-collapse-item title="bibtex" name="7">
            <div>{{ essay.Bibtex }}</div>
          </el-collapse-item>
          <el-collapse-item title="github" name="8" @click="getGithubInfo">
            <el-table :data="githubDisplayed">
              <el-table-column prop="attribute" />
              <el-table-column prop="value" />
            </el-table>
          </el-collapse-item>
          <el-collapse-item title="引用论文" name="9" @click="getRevelanceInfo">
            <el-table :data="revelanceDisplayed">
              <el-table-column prop="attribute" />
              <el-table-column prop="value" />
            </el-table>
          </el-collapse-item>
          <el-collapse-item title="视频链接" name="10">
            <div>{{ essay.VideoHref }}</div>
          </el-collapse-item>
          <el-collapse-item title="arxiv链接" name="11">
            <div>{{ essay.ArxivHref }}</div>
          </el-collapse-item>
        </el-collapse>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import {
  HomeFilled,
  Eleme,
  ElemeFilled,
  Star,
  StarFilled,
} from "@element-plus/icons";
import router from "@/router/index";
import store from "@/store";
import AsidePart1 from "./AsidePart1.vue";
export default {
  name: "EssayInfo",
  components: {
    AsidePart1,
    HomeFilled,
    Eleme,
    ElemeFilled,
    Star,
    StarFilled,
  },
  data() {
    return {
      githubDisplayed: [],
      revelanceDisplayed: [],
      liked: false,
      collected: false,
    };
  },
  methods: {
    getGithubInfo() {},
    getRevelanceInfo() {},
    RouteToHome() {
      router.push({ name: "home" });
    },
    Like() {
      this.liked = !this.liked;
    },
    Collect() {
      this.collected = !this.collected;
    },
  },
  computed: {
    essay() {
      return store.state.essayS;
    },
  },
};
</script>
<style scoped>
.title {
  font-size: x-large;
}
.elementRight {
  font-size: x-large;
  margin: 5px;
}
.el-collapse {
  font-size: x-large;
}
</style>
