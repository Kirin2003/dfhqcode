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
          <el-input v-model="essaySearched" placeholder="论文信息">
            <template #append>
              <el-button>
                <el-icon><search /></el-icon>
              </el-button>
            </template>
          </el-input>
        </el-header>
        <p class="sort" :sort="Sort">"{{ Sort }}"论文</p>
        <el-table class="essays" :data="Essays" row-style="height: 50px">
          <el-table-column prop="title" label="题目" />
          <el-table-column prop="writers" label="作者" />
        </el-table>
        <el-footer>
          <div>
            <el-pagination
              :page-sizes="[100, 200, 300, 400]"
              :page-size="100"
              layout="total, prev, pager, next"
              :total="400"
              @current-change="handleCurrentChange"
            >
            </el-pagination>
          </div>
        </el-footer>
      </el-container>
    </el-container>
  </div>
</template>
<script>
import { Search, UserFilled, Document } from "@element-plus/icons";
import router from "@/router/index";
export default {
  name: "HotWord",
  components: {
    Search,
    UserFilled,
    Document,
  },
  data() {
    return {
      UserName: "heziang",
      AllEssays: [
        {
          title:
            "Image-to-Image Translation with Conditional Adversarial Networks",
          writers: "Phillip Isola, Jun-Yan Zhu, Tinghui Zhou, Alexei A. Efros",
        },
        {
          title:
            "Unpaired Image-to-Image Translation Using Cycle-Consistent Adversarial Networks",
          writers: "Jun-Yan Zhu, Taesung Park Alexei A. Efros 2017",
        },
        {
          title:
            "Very Deep Convolutional Networks for Large-Scale Image Recognition",
          writers: "K. Simonyan, Andrew Zisserman",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
      ], //五个论文一组的数组
      Essays: [
        {
          title:
            "Image-to-Image Translation with Conditional Adversarial Networks",
          writers: "Phillip Isola, Jun-Yan Zhu, Tinghui Zhou, Alexei A. Efros",
        },
        {
          title:
            "Unpaired Image-to-Image Translation Using Cycle-Consistent Adversarial Networks",
          writers: "Jun-Yan Zhu, Taesung Park Alexei A. Efros 2017",
        },
        {
          title:
            "Very Deep Convolutional Networks for Large-Scale Image Recognition",
          writers: "K. Simonyan, Andrew Zisserman",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
      ], //一组论文
      Sort: "default",
      //EssayNum: this.Essays.length * 5,
      essaySearched: "",
      HotWordsOfIccv: [
        { rank: 1, word: "image" },
        { rank: 2, word: "automatic" },
        { rank: 3, word: "color" },
        { rank: 4, word: "accuracy" },
        { rank: 5, word: "AI" },
      ],
      HotWordsOfCvpr: [],
      HotWordsOfEccv: [],
    };
  },
  methods: {
    RouteToUserInfo() {
      router.push({ name: "User", params: { userName: this.UserName } });
    },
  },
};
</script>
<style scoped>
.el-aside {
  background-color: antiquewhite;
  width: 500px;
}
.essays {
  font-size: 20px;
}
</style>
