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
            <template #prepend>
              <el-select
                v-model="select"
                placeholder="相关度"
                default-first-option="true"
                width="125px"
              >
                <el-option label="相关度" value="revelance" />
                <el-option label="热度" value="hotnum" />
                <el-option label="时间" value="time" />
              </el-select>
            </template>
            <template #append>
              <el-button>
                <el-icon><search /></el-icon>
              </el-button>
            </template>
          </el-input>
        </el-header>
        <p class="sort" :sort="Sort">"{{ Sort }}"论文</p>
        <el-table
          class="essays"
          :data="Essays"
          row-style="height: 100px"
          @row-click="RouteToEssayInfo"
        >
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
import store from "@/store/";
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
      select: "",
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
          Id: "134153413",
          title:
            "Image-to-Image Translation with Conditional Adversarial Networks",
          Authors: "Phillip Isola, Jun-Yan Zhu, Tinghui Zhou, Alexei A. Efros",
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
      essaySelected: {},
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
    RouteToEssayInfo(row) {
      this.essaySelected = row;
      store.commit("SelectEssay", this.essaySelected);
      //还要传递论文信息
      router.push({
        name: "Essay",
        params: { essayId: this.essaySelected.Id },
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
