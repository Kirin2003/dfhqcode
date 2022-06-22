<template>
  <div class="common-layout">
    <el-container>
      <el-aside>
        <el-avatar @click="RouteToUserInfo"><user-filled /></el-avatar>
        <p class="hotword">热词榜</p>
        <el-menu>
          <el-sub-menu index="1">
            <template #title>
              <el-icon><Document /></el-icon>ICCV
            </template>
            <el-menu-item-group>
              <el-menu-item
                v-for="hotword in HotWordsOfIccv"
                :key="hotword.rank"
                @click="ShowWordEssay"
              >
                <!--展示对应热词的相关论文 -->
                {{ hotword.word }}</el-menu-item
              >
            </el-menu-item-group>
          </el-sub-menu>
          <el-sub-menu index="2">
            <template #title>
              <el-icon><Document /></el-icon>CVPR
            </template>
            <el-menu-item-group>
              <el-menu-item
                v-for="hotword in HotWordsOfCvpr"
                :key="hotword.rank"
                index="2-"
                +
                hotwords.rank
                @click="ShowWordEssay"
              >
                <!--展示对应热词的相关论文 -->
                {{ hotword.word }}</el-menu-item
              >
            </el-menu-item-group>
          </el-sub-menu>
          <el-sub-menu index="3">
            <template #title>
              <el-icon><Document /></el-icon>ECCV
            </template>
            <el-menu-item-group>
              <el-menu-item
                v-for="hotword in HotWordsOfEccv"
                :key="hotword.rank"
                index="3-"
                +
                hotword.rank
                @click="ShowWordEssay"
              >
                <!--展示对应热词的相关论文 -->
                {{ hotword.word }}</el-menu-item
              >
            </el-menu-item-group>
          </el-sub-menu>
        </el-menu>
      </el-aside>
      <el-container>
        <el-header>
          <p style="text-align: right">
            <el-icon class="elementRight" @click="RouteToHome"
              ><HomeFilled
            /></el-icon>
          </p>
        </el-header>
        <div>
          <p font-size="20px">
            {{ essay.title }}
          </p>
        </div>
        <el-collapse>
          <el-collapse-item title="作者" name="1">
            <div>{{ essay.Authors }}</div>
          </el-collapse-item>
          <el-collapse-item title="摘要" name="2">
            <div>{{ essay.PaperAbstract }}</div>
          </el-collapse-item>
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
import { HomeFilled, UserFilled, Document } from "@element-plus/icons";
import router from "@/router/index";
import store from "@/store";
export default {
  name: "EssayInfo",
  components: { HomeFilled, UserFilled, Document },
  data() {
    return {
      githubDisplayed: [],
      revelanceDisplayed: [],
    };
  },
  methods: {
    getGithubInfo() {},
    getRevelanceInfo() {},
    RouteToHome() {
      router.push({ name: "home" });
    },
  },
  computed: {
    essay() {
      return store.state.essayS;
    },
  },
};
</script>
