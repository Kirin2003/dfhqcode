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
              <el-table-column prop="name" label="github仓库名" />
              <el-table-column prop="url" />
            </el-table>
          </el-collapse-item>
          <el-collapse-item title="引用论文" name="9" @click="getRevelanceInfo">
            <el-table :data="revelanceDisplayed">
              <el-table-column prop="title" label="题目" />
              <el-table-column prop="authors" label="引用作者" />
              <el-table-column prop="paperHref" label="引用论文url" />
            </el-table>
            <el-table :data="citationDisplayed">
              <el-table-column prop="title" label="题目" />
              <el-table-column prop="authors" label="被引用作者" />
              <el-table-column prop="paperHref" label="被引用论文url" />
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
import {
  judgeCollection,
  addCollection,
  subCollection,
  judgeLike,
  addLike,
  subLike,
  getGithubInfo,
  getCitation,
  getRevelance,
} from "@/axios/axios";
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
      githubDisplayed: {},
      revelanceDisplayed: [],
      citationDisplayed: [],
      liked: false,
      collected: false,
    };
  },
  mounted() {
    var info = {
      userId: this.userId,
      paperId: this.essay.Id,
    };
    judgeCollection(info).then((res) => {
      if (res.status === 200) {
        this.collected = true;
      } else if (res.status === 404) {
        this.collected = false;
      }
    });
    judgeLike(info).then((res) => {
      if (res.status === 200) {
        this.liked = true;
      } else if (res.status === 404) {
        this.liked = false;
      }
    });
    getGithubInfo(this.essay.Id).then((res) => {
      this.githubDisplayed = res.data;
    });
    getRevelance(this.essay.Id).then((res) => {
      this.revelanceDisplayed = res.data.revelance;
    });
    getCitation(this.essay.Id).then((res) => {
      this.citationDisplayed = res.data.citation;
    });
  },
  methods: {
    getGithubInfo() {},
    getRevelanceInfo() {},
    RouteToHome() {
      router.push({ name: "home" });
    },
    Like() {
      var info = {
        userId: this.userId,
        paperId: this.essay.Id,
      };
      if (this.liked === true) {
        subLike(info);
      } else {
        addLike(info);
      }
      this.liked = !this.liked;
    },
    Collect() {
      var info = {
        userId: this.userId,
        paperId: this.essay.Id,
      };
      if (this.collected === true) {
        subCollection(info);
      } else {
        addCollection(info);
      }
      this.collected = !this.collected;
    },
  },
  computed: {
    essay() {
      return store.state.essayS;
    },
    userId() {
      return store.state.userId;
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
